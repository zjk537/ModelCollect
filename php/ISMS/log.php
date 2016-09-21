<?php
/**
 * 日志处理类
 * 
 * @date 2015.09.14
 * @author zjk
 * 
 */

date_default_timezone_set('prc'); // set default timezone
class Log{

    //单例模式
    private static $instance    = NULL;
    //文件句柄
    private static $handle      = NULL;
    //日志开关
    private $log_switch     = 1;
    //日志文件最大长度，超出长度重新建立文件
    private $log_max_size = '3M'; // G|M|K|B

    // 日志文件最多保留近30天
    private $log_bak_size = 30;
    //日志保存路径
    private $log_path = "/export/htdocs/services/logs/";

    private static $log_tag = 'phplog';


    /**
     * 构造函数
     * 
     * @date 2014.02.04
     * @author zjk
     */
    protected function __construct(){
        //注意：以下是配置文件中的常量，请读者自行更改
        //$this -> log_path = "d:/logs/";
        //$this -> log_max_size = '3M';
    }

    /**
     * 单利模式
     * 
     * @date 2014.02.04
     * @author zjk
     */
    public static function get_instance($log_tag = NULL){
        if(!self::$instance instanceof self){
            if($log_tag != NULL && $log_tag != ""){
                self::$log_tag = $log_tag;
            }
            self::$instance = new self;
        }
        return self::$instance;
    }


    public function log_debug($message,$exception = NULL){
        $this -> log("DEBUG",$message,$exception);
    }

    public function log_info($message,$exception = NULL){
       $this -> log("INFO",$message,$exception);
    }

    public function log_error($message,$exception = NULL){
        $this -> log("ERROR",$message,$exception);
    }

    public function log_fatal($message,$exception = NULL){
        $this -> log("FATAL",$message,$exception);
        
    }

    public function log_warn($message,$exception = NULL){
        $this -> log("WARN",$message,$exception);
    }
   

    // 记录日志 
    private function log($type,$message,$exception = NULL){
        if(!$this -> log_switch){
            return;
        }
        
        $log_file = $this -> log_path . self::$log_tag . "/log.log";
        
        //clearstatcache();
        if(!is_writable($this -> log_path)){
            return;
        }
        $this -> open_log_file($log_file);

        $back_trace = $this -> get_backtrace();
        $prev_method = 'unknown';
        ///#3.*->(.*)\(/
        if(preg_match('/#3\s*(.*)\(/', $back_trace, $matches)){
            $prev_method = $matches[1];
        }
        $prev_method = str_pad($prev_method,30," ",STR_PAD_RIGHT);

        $time_str = date('Y-m-d H:i:s,').str_pad(floor(microtime()*1000),3,' ',STR_PAD_RIGHT);

        $msgType =strtolower(gettype($message));
        $msg = $message;
        if($msgType == 'object' || $msgType == 'array'){
            $msg = json_encode($message);
        }
        //.self::$log_tag. "    "
        fwrite(self::$handle, $type."    ".$time_str."      ".$prev_method."        ".$msg."\r\n");
        if($exception != NULL && $exception != ""){
            $exMsg = $exception -> getMessage()."\r\n".$exception -> getTraceAsString()."\r\n";
            fwrite(self::$handle, $exMsg);
        }

        //fclose(self::$handle);
    }

   /**
     * 获取堆栈信息
     * 
     * @date 2014.02.04
     * @author zjk
     */
    private function get_backtrace() {
      ob_start();
      debug_print_backtrace();
      return ob_get_clean();
    }
    
    
    // 打开日志文件句柄，以及昨天文件重命名，文件太大重命名
    private function open_log_file($log_file){
        $log_dir = dirname($log_file);
        if(!file_exists($log_file)){            
            if(!is_dir($log_dir)){
                mkdir($log_dir, 0777, true);
            }
            if(self::$handle == NULL){
                self::$handle = fopen($log_file, 'a');
            }
            return;
        } 

        $dirHandler = opendir($log_dir);
        $log_cnt = 0; $log_bak_cnt = 0;
        while (($file = readdir($dirHandler)) != FALSE) {
            if($file == '.' || $file == '..'){
                continue;
            }
            $tmp_file = $log_dir."/".$file;
            if(!preg_match("/log\d*\.log/i", $file)){
                // delete history log baks
                $fileCDate = date('Ymd', filectime($tmp_file));
                $historyDate = date('Ymd',strtotime("-".$this -> log_bak_size." days"));
                if($fileCDate < $historyDate){
                    unlink($tmp_file);
                }
                $log_bak_cnt++;
                continue;
            }

            
            // rename log file from log.log to log_Ymd.log log_Ymd_1.log....log_Ymd_N.log
            clearstatcache();
            $cd = date('Ymd', filectime($tmp_file));
            $curDate = date('Ymd');
            if($cd < $curDate){
                $newName = substr($tmp_file,0,strripos($tmp_file,'.')).'_'.$cd.'.log';
                $nameIndex = 0;
                while (file_exists($newName)) {
                    $nameIndex++;
                    $newName = substr($tmp_file,0,strripos($tmp_file,'.')).'_'.$cd.'_'.$nameIndex.'.log';
                }
                rename($tmp_file, $newName);
                continue;
            }

            $log_cnt++;
        }
        
        if(!file_exists($log_file)){
            self::$handle = fopen($log_file, 'a');
        }

        $log_size = filesize($log_file);
        $max_size = $this -> get_max_size();
        // rename log file from log.log to log1.log...logN.log
        if($log_cnt > 0 && $log_size >= $max_size){
            rename($log_file, substr($log_file,0,strripos($log_file,'.')).$log_cnt.'.log');
        }
        
        closedir($dirHandler);

        if(self::$handle == NULL){
            self::$handle = fopen($log_file, 'a');
        }
        
    }

    private function get_max_size(){
        $unit = strtoupper(substr($this -> log_max_size, -1));
        $value = intval(substr($this -> log_max_size, 0, -1));
        $result = 0;
        switch ($unit) {
            case 'G':
                $result = $value * 1024 * 1024 * 1024;
                break;
            case 'M':
                $result = $value * 1024 * 1024;
                break;
            case 'K':
                $result = $value * 1024;
                break;
            default:
                $result = $value;
                break;
        }
        return $result;
    }

}
?>