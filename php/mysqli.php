<?php
#==================================================================================================
# Filename: /db/db_mysqli.php
# Note : 连接数据库类，MySQLi版
#==================================================================================================
#[类库sql]
class db_mysqli
{
    public $query_count = 0;
    public $host;
    public $user;
    public $pass;
    public $data;
    public $conn;
    public $result;
    public $prefix = "qinggan_";
    //返回结果集类型，默认是数字+字符
    public $rs_type     = MYSQLI_ASSOC;
    public $query_times = 0; #[查询时间]
    public $conn_times  = 0; #[连接数据库时间]
    public $unbuffered  = false;
    //定义查询列表
    public $querylist;
    public $debug = false;
    #[构造函数]
    public function __construct($config = array())
    {
        $this->host   = $config['host'] ? $config['host'] : 'localhost';
        $this->port   = $config['port'] ? $config['port'] : '3306';
        $this->user   = $config['user'] ? $config['user'] : 'root';
        $this->pass   = $config['pass'] ? $config['pass'] : '';
        $this->data   = $config['data'] ? $config['data'] : '';
        $this->debug  = $config["debug"] ? $config["debug"] : false;
        $this->prefix = $config['prefix'] ? $config['prefix'] : 'qinggan_';
        if ($this->data) {
            $ifconnect = $this->connect($this->data);
            if (!$ifconnect) {
                $this->conn = false;
                return false;
            }
        }
        return true;
    }
    #[兼容PHP4]
    public function db_mysqli($config = array())
    {
        return $this->__construct($config);
    }
    #[连接数据库]
    public function connect($database = "")
    {
        $start_time = $this->time_used();
        if (!$this->port) {
            $this->port = "3306";
        }

        $this->conn = @mysqli_connect($this->host, $this->user, $this->pass, "", $this->port) or false;
        if (!$this->conn) {
            return false;
        }
        $version = $this->get_version();
        if ($version > "4.1") {
            mysqli_query($this->conn, "SET NAMES 'utf8'");
            if ($version > "5.0.1") {
                mysqli_query($this->conn, "SET sql_mode=''");
            }
        }
        $end_time = $this->time_used();
        $this->conn_times += round($end_time - $start_time, 5); #[连接数据库的时间]
        $ifok = $this->select_db($database);
        return $ifok ? true : false;
    }
    public function select_db($data = "")
    {
        $database = $data ? $data : $this->data;
        if (!$database) {
            return false;
        }
        $this->data = $database;
        $start_time = $this->time_used();
        $ifok       = mysqli_select_db($this->conn, $database);
        if (!$ifok) {
            return false;
        }
        $end_time = $this->time_used();
        $this->conn_times += round($end_time - $start_time, 5); #[连接数据库的时间]
        return true;
    }
    #[关闭数据库连接，当您使用持续连接时该功能失效]
    public function close()
    {
        if (is_resource($this->conn)) {
            return mysqli_close($this->conn);
        } else {
            return true;
        }
    }
    public function __destruct()
    {
        return $this->close();
    }
    public function set($name, $value)
    {
        if ($name == "rs_type") {
            $value = strtolower($value) == "num" ? MYSQLI_NUM : MYSQLI_ASSOC;
        }
        $this->$name = $value;
    }
    public function query($sql)
    {
        if (!is_resource($this->conn)) {
            $this->connect();
        } else {
            if (!mysql_ping($this->conn)) {
                $this->close();
                $this->connect();
            }
        }
        if ($this->debug) {
            $sqlkey = md5($sql);
            if ($this->querylist) {
                $qlist = array_keys($this->querylist);
                if (in_array($sqlkey, $qlist)) {
                    $count                    = $this->querylist[$sqlkey]["count"] + 1;
                    $this->querylist[$sqlkey] = array("sql" => $sql, "count" => $count);
                } else {
                    $this->querylist[$sqlkey] = array("sql" => $sql, "count" => 1);
                }
            } else {
                $this->querylist[$sqlkey] = array("sql" => $sql, "count" => 1);
            }
        }
        $start_time   = $this->time_used();
        $func         = $this->unbuffered && function_exists("mysqli_multi_query") ? "mysqli_multi_query" : "mysqli_query";
        $this->result = @$func($this->conn, $sql);
        $this->query_count++;
        $end_time = $this->time_used();
        $this->query_times += round($end_time - $start_time, 5); #[查询时间]
        if (!$this->result) {
            return false;
        }
        return $this->result;
    }
    public function get_all($sql = "", $primary = "")
    {
        $result = $sql ? $this->query($sql) : $this->result;
        if (!$result) {
            return false;
        }
        $start_time = $this->time_used();
        $rs         = array();
        $is_rs      = false;
        while ($rows = mysqli_fetch_array($result, $this->rs_type)) {
            if ($primary && $rows[$primary]) {
                $rs[$rows[$primary]] = $rows;
            } else {
                $rs[] = $rows;
            }
            $is_rs = true;
        }
        $end_time = $this->time_used();
        $this->query_times += round($end_time - $start_time, 5); #[查询时间]
        return ($is_rs ? $rs : false);
    }
    public function get_one($sql = "")
    {
        $start_time = $this->time_used();
        $result     = $sql ? $this->query($sql) : $this->result;
        if (!$result) {
            return false;
        }
        $rows     = mysqli_fetch_array($result, $this->rs_type);
        $end_time = $this->time_used();
        $this->query_times += round($end_time - $start_time, 5); #[查询时间]
        return $rows;
    }
    public function insert_id($sql = "")
    {
        if ($sql) {
            $rs = $this->get_one($sql);
            return $rs;
        } else {
            return mysqli_insert_id($this->conn);
        }
    }
    public function insert($sql)
    {
        $this->result = $this->query($sql);
        $id           = $this->insert_id();
        return $id;
    }
    public function all_array($table, $condition = "", $orderby = "")
    {
        if (!$table) {
            return false;
        }
        $table = $this->prefix . $table;
        $sql   = "SELECT * FROM " . $table;
        if ($condition && is_array($condition) && count($condition) > 0) {
            $sql_fields = array();
            foreach ($condition as $key => $value) {
                $sql_fields[] = "`" . $key . "`='" . $value . "' ";
            }
            $sql .= " WHERE " . implode(" AND ", $sql_fields);
        }
        if ($orderby) {
            $sql .= " ORDER BY " . $orderby;
        }
        $rslist = $this->get_all($sql);
        return $rslist;
    }
    public function one_array($table, $condition = "")
    {
        if (!$table) {
            return false;
        }
        $table = $this->prefix . $table;
        $sql   = "SELECT * FROM " . $table;
        if ($condition && is_array($condition) && count($condition) > 0) {
            $sql_fields = array();
            foreach ($condition as $key => $value) {
                $sql_fields[] = "`" . $key . "`='" . $value . "' ";
            }
            $sql .= " WHERE " . implode(" AND ", $sql_fields);
        }
        $rslist = $this->get_one($sql);
        return $rslist;
    }
    //将数组写入数据中
    public function insert_array($data, $table, $insert_type = "insert")
    {
        if (!$table || !is_array($data) || !$data) {
            return false;
        }
        $table = $this->prefix . $table; //自动增加表前缀
        if ($insert_type == "insert") {
            $sql = "INSERT INTO " . $table;
        } else {
            $sql = "REPLACE INTO " . $table;
        }
        $sql_fields = array();
        $sql_val    = array();
        foreach ($data as $key => $value) {
            $sql_fields[] = "`" . $key . "`";
            $sql_val[]    = "'" . $value . "'";
        }
        $sql .= "(" . (implode(",", $sql_fields)) . ") VALUES(" . (implode(",", $sql_val)) . ")";
        return $this->insert($sql);
    }
    //更新数据
    public function update_array($data, $table, $condition)
    {
        if (!$data || !$table || !$condition || !is_array($data) || !is_array($condition)) {
            return false;
        }
        $table      = $this->prefix . $table; //自动增加表前缀
        $sql        = "UPDATE " . $table . " SET ";
        $sql_fields = array();
        foreach ($data as $key => $value) {
            $sql_fields[] = "`" . $key . "`='" . $value . "'";
        }
        $sql .= implode(",", $sql_fields);
        $sql_fields = array();
        foreach ($condition as $key => $value) {
            $sql_fields[] = "`" . $key . "`='" . $value . "' ";
        }
        $sql .= " WHERE " . implode(" AND ", $sql_fields);
        return $this->query($sql);
    }
    public function count($sql = "")
    {
        if ($sql) {
            $this->rs_type = MYSQLI_NUM;
            $this->query($sql);
            $rs            = $this->get_one();
            $this->rs_type = MYSQLI_ASSOC;
            return $rs[0];
        } else {
            return mysqli_num_rows($this->result);
        }
    }
    public function num_fields($sql = "")
    {
        if ($sql) {
            $this->query($sql);
        }
        return mysqli_num_fields($this->result);
    }
    public function list_fields($table)
    {
        $rs = $this->get_all("SHOW COLUMNS FROM " . $table);
        if (!$rs) {
            return false;
        }
        foreach ($rs as $key => $value) {
            $rslist[] = $value["Field"];
        }
        return $rslist;
    }
    #[显示表名]
    public function list_tables()
    {
        $rs = $this->get_all("SHOW TABLES");
        return $rs;
    }
    public function table_name($table_list, $i)
    {
        return $table_list[$i];
    }
    public function escape_string($char)
    {
        if (!$char) {
            return false;
        }
        return mysqli_escape_string($this->conn, $char);
    }
    public function get_version()
    {
        return mysqli_get_server_info($this->conn);
    }
    public function time_used()
    {
        $time      = explode(" ", microtime());
        $used_time = $time[0] + $time[1];
        return $used_time;
    }
    //Mysql的查询时间
    public function conn_times()
    {
        return $this->conn_times + $this->query_times;
    }
    //MySQL查询资料
    public function conn_count()
    {
        return $this->query_count;
    }
    # 高效SQL生成查询，仅适合单表查询
    public function phpok_one($tbl, $condition = "", $fields = "*")
    {
        $sql = "SELECT " . $fields . " FROM " . $this->db->prefix . $tbl;
        if ($condition) {
            $sql .= " WHERE " . $condition;
        }
        return $this->get_one($sql);
    }
    public function debug()
    {
        if (!$this->querylist || !is_array($this->querylist) || count($this->querylist) < 1) {
            return false;
        }
        $html = '<table cellpadding="0" cellspacing="0" width="100%" bgcolor="#CECECE"><tr><td>';
        $html .= '<table cellpadding="1" cellspacing="1" width="100%">';
        $html .= '<tr><th bgcolor="#EFEFEF" height="30px">SQL</th><th bgcolor="#EFEFEF" width="80px">查询</th></tr>';
        foreach ($this->querylist as $key => $value) {
            $html .= '<tr><td bgcolor="#FFFFFF"><div style="padding:3px;color:#6E6E6E;">' . $value['sql'] . '</div></td>';
            $html .= '<td align="center" bgcolor="#FFFFFF"><div style="padding:3px;color:#000000;">' . $value["count"] . '</div></td></tr>';
        }
        $html .= "</table>";
        $html .= "</td></tr></table>";
        return $html;
    }
    public function conn_status()
    {
        if (!$this->conn) {
            return false;
        }

        return true;
    }
}
