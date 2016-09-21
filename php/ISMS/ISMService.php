<?php

/**
	* Desc: ji xin tong short message services for CBN Week
	* Author: zjk
	* date: 2015-9-1
	*/

	session_start(); 
	date_default_timezone_set('prc'); // set default timezone
	require_once("log.php");
	class ISMService{
		private $uid = 'cbnyicaiwang';
		private $pwd = '998877';
		private $serviceUri = 'http://service2.winic.org:8003/Service.asmx?WSDL';
		private $log = NULL;

		public function __construct() {     
	        $this -> log = Log::get_instance('isms');    
	    }  

		private function getConnect(){
			require_once('sql.php');
			$db_mysql = new db_mysql();
			$db_mysql -> connect('localhost','smslog','1qaz@WSX','sms_log',0,'utf-8');
			return $db_mysql;
		}

		// generate validate code
		private function generateCode($length = 6) {			
		    return rand(pow(10,($length-1)), pow(10,$length)-1);
		}
		
		// validate sms code
		public function validateCode(){
			//  method type validation
			if($_SERVER['REQUEST_METHOD'] != "POST"){
				return;
			}
			//将获取的缓存时间转换成时间戳加上60 * 2秒后与当前时间比较，小于当前时间即为过期
			if((strtotime($_SESSION['time']) + 60 * 2) < time()) {
		        session_destroy();
		        unset($_SESSION);
				$arrResult = array('resultCode' => 0, 'resultMsg' => "验证码已过期" );
				echo json_encode($arrResult);
		        return;
		    }

			$postData = json_decode(file_get_contents("php://input"),1);
			$mcode = trim($postData['mcode']);
			if($mcode == $_SESSION['mcode']){
				$arrResult = array('resultCode' => 1, 'resultMsg' => "验证成功" );
				echo json_encode($arrResult);
				return;
			}
			$arrResult = array('resultCode' => 0, 'resultMsg' => "验证码错误" );
			echo json_encode($arrResult);
		}

		private function phoneValidate($phone){
			$phoneRegex = '/^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/';
			return preg_match($phoneRegex, $phone);			    
		}

		// set validate cache time
		private function setSession(){
			if (isset($_SESSION['time'])){//判断缓存时间
		        session_id();
		        $_SESSION['time'];
		    } else {
		        $_SESSION['time'] = date("Y-m-d H:i:s");
		    }
		}

		// send message post
		public function sendMessage(){
			//  method type validation
			if($_SERVER['REQUEST_METHOD'] != "POST"){
				return;
			}

			$postData = json_decode(file_get_contents("php://input"),1);
			$this -> log -> log_info("请求参数：".json_encode($postData));

			$phone = $postData['phone'];
			$msg = isset($postData['msg']) ? $postData['msg'] : 'Request CBNWeek SMS Validate Code %s';			
			$otime = isset($postData['otime']) ? $postData['otime'] : '';	

			// empty params validation
			if($phone == ""){
				echo json_encode(array('resultMsg' => "phone can't be empty")) ;
				$this -> log -> log_info("phone can't be empty");
				return;
			}
			if($this -> phoneValidate($phone)){
				echo json_encode(array('resultMsg' => "invalid phone number")) ;
				$this -> log -> log_info("invalid phone number");
				return;
			}

			// 同一个手机号，两次获取短信验证码时间不超过60*2 秒
			$lastSMSTime = $this -> getLastSMSTime($phone);
			if((strtotime($lastSMSTime) + 60 * 2) > time()) {
				echo json_encode(array('resultMsg' => "您的请求太频繁,请2分钟后重试!")) ;
				return;
			}

			$_SESSION['mcode'] = $this -> generateCode();
			$msg = sprintf($msg, $_SESSION['mcode']);
			try {
				// $client = new SoapClient($this -> serviceUri);
				// $smsObj = array('uid' => $this -> uid,
				// 	'pwd' => $this -> pwd,
				// 	'tos' => $phone,
				// 	'msg' => $msg,
				// 	'otime' => $otime);
				// $result = $client -> __soapCall('SendMessages',array('parameters' => $smsObj));


				$result = $this -> sendSMS($phone,$msg);
				$resultArr = explode('/',$result[0]);
				$resultCode = $resultArr[0];

			} catch (Exception $e) {
				$this -> log -> log_error("请求短信接口出错：",$e);
				$resultCode = "-99";

			}
			if($resultCode == "000"){
				$this -> setSession();
			}
			$resultMsg = $this -> getErrorMsg($resultCode);
			$this -> log -> log_info(array('phone' => $phone, 
											'msg' => $msg, 
											'otime' => $otime, 
											'resultCode' => $resultCode, 
											'resultMsg' => $resultMsg));
			$this -> saveSMSLog($phone, $msg, $otime,$resultCode, $resultMsg);
			echo json_encode(array('resultCode' => $resultCode , 'resultMsg' => $resultMsg));
		}

		
		private function sendSMS($strMobile,$content){
			// linux get
			$url="http://service.winic.org:8009/sys_port/gateway/?id=%s&pwd=%s&to=%s&content=%s&time=";
			$id = urlencode($this -> uid);
			$pwd = urlencode($this -> pwd);
			$to = urlencode($strMobile);
			$content = urlencode($content);
			$rurl = sprintf($url, $id, $pwd, $to, $content);
			return file($rurl);
		}

		// save sms log history
		private function saveSMSLog($phone, $msg, $otime,$resultCode, $resultMsg){
			$dbConn = $this -> getConnect();
			if(!$dbConn){
				$this -> log -> log_info('数据连接失败！');
				echo json_encode(array('resultMsg' => '数据连接失败！'));
				return;
			}

			$smsLogArray = array(
				'phone' => $phone,
				'type' => 'CBNWeek',
				'msg' => $msg,
				'otime' => $otime,
				'result_code' => $resultCode,
				'result_msg' => $resultMsg,
				'create_date' => date("Y-m-d H:i:s")
			);
			
			$dbConn -> insert("sms_logs", $smsLogArray);
			$dbConn -> close();
		}

		private function getLastSMSTime($phone){
			$dbConn = $this -> getConnect();
			if(!$dbConn){
				$this -> log -> log_info('数据连接失败！'); ;
				echo json_encode(array('resultMsg' => '数据连接失败！'));
				return;
			}
			$lastSendTime = $dbConn -> select("select create_date from sms_logs where phone = '".$phone."' order by id desc limit 1");
			if($lastSendTime){
				return $lastSendTime[0]['create_date'];
			}
			$curDate = date('Y-m-d H:i:s');
			return date('Y-m-d H:i:s',strtotime('$curDate -1 days'));

		}
		// get result code desc
		private function getErrorMsg($resultCode){
			$resultMsg = '';
			switch ($resultCode) {
				case '000':
					$resultMsg = '成功';
					break;
				case '-01':
					$resultMsg = '当前账号余额不足';
					break;
				case '-02':
					$resultMsg = '当前用户ID错误';
					break;
				case '-03':
					$resultMsg = '当前密码错误';
					break;
				case '-04':
					$resultMsg = '参数不够或参数内容的类型错误';
				case '-05':
					$resultMsg = '手机号码格式不对';
					break;
				case '-06':
					$resultMsg = '短信内容编码不对';
					break;
				case '-07':
					$resultMsg = '短信内容含有敏感字符';
					break;
				case '-08':
					$resultMsg = '无接收数据';
					break;
				case '-09':
					$resultMsg = '系统维护中...';
				case '-10':
					$resultMsg = '手机号码数量超长';
				case '-11':
					$resultMsg = '短信内容超长！（70个字符）';
				case '-12':
					$resultMsg = '其它错误';
				case '-13':
					$resultMsg = '文件传输错误';
					break;
				default:
					$resultMsg = '读取接口失败';
					break;
			}
			return $resultMsg;
		}

	}

	$service = new ISMService();
	$action = $_REQUEST['act'];
	if(method_exists($service,$action)){
		$service -> $action();
	}
?>