<?php
class mail_box_select
{
        function mail_box_select()
        {
                
        }
        
        function select_a_box_by_email($email)
        {
                $email = strtolower($email);
               $ifhot = $this->if_a_box_is_hotmail($email);
               $ifyahoo = $this->if_a_box_is_yahoo($email);
               $ifsohu = $this->if_a_box_is_sohu($email);
               //$ifsohu = $this->if_a_box_is_sohu($email);
               $ifdefault = $this->if_a_box_is_default($email);
               
               if ($ifhot==1)
               {
                     $row_mail = $this->get_a_hotmail_box($email);
               }
               else if ($ifyahoo==1)
               {
                     $row_mail = $this->get_a_yahoo_box($email);
               }
               else if ($ifsohu==1)
               {
                     $row_mail = $this->get_a_sohu_box($email);
               }
               else if ($ifdefault==1)
               {
                     $row_mail = $this->get_a_default_box($email);
               }
               else
               {
                     $row_mail = $this->get_a_other_box($email);
               }
               return $row_mail;
        }
        
        function if_a_box_is_hotmail($email)
        {
               $arr_e = explode("@",$email);
               $domain = $arr_e[1];
               $arr_hotmail = array("msn.com","msn.cn","hotmail.com","live.com","live.cn","outlook.com");
               if (in_array($domain,$arr_hotmail))
               {
                    return 1;
               }
               else
               {
                    return 0;
               }
        }
        
        function if_a_box_is_yahoo($email)
        {
               $arr_e = explode("@",$email);
               $domain = $arr_e[1];
               $arr_yahoo = array("yahoo.cn","yahoo.com.cn","yahoo.com");
               if (in_array($domain,$arr_yahoo))
               {
                    return 1;
               }
               else
               {
                    return 0;
               }
        }
        
        function if_a_box_is_sohu($email)
        {
               $arr_e = explode("@",$email);
               $domain = $arr_e[1];
               $arr_sohu = array("sohu.com");
               if (in_array($domain,$arr_sohu))
               {
                    return 1;
               }
               else
               {
                    return 0;
               }
        }
        
        function if_a_box_is_default($email)
        {
               $arr_e = explode("@",$email);
               $domain = $arr_e[1];
$arr_default = array("163.com",
"126.com",
"sina.com",
"sina.cn",
"me.com",
"sina.com.cn",
"gmail.com",
"qq.com",
"foxmail.com",
"21cn.com",
"yeah.com",
"139.com");
               if (in_array($domain,$arr_default))
               {
                    return 1;
               }
               else
               {
                    return 0;
               }
        }
        
        
        function get_a_hotmail_box($email)
        {
                $md5 = md5($email);
                $suffix = substr($md5, -1, 1);
                //echo $md5."----".$suffix."\n";
                $arr_0 = array("0","1","2","3");
                $arr_1 = array("4","5","6");
                $arr_2 = array("7","8","9");
                $arr_3 = array("a","b","c");
                $arr_4 = array("d","e","f");
                if (in_array($suffix,$arr_0))
                {
                       $username="cbnweekha";
                }
                else if (in_array($suffix,$arr_1))
                {
                       $username="cbnweekhb";
                       //$username="cbnweekha";
                }
                else if (in_array($suffix,$arr_2))
                {
                       $username="cbnweekhc";
                       //$username="cbnweekha";
                }
                else if (in_array($suffix,$arr_3))
                {
                       $username="cbnweekhd";
                       //$username="cbnweekha";
                }
                else
                {
                       $username="cbnweekhe";
                       //$username="cbnweekha";
                }
                /*
               $row_other = array(
                        "host"=>"smtp.163.com",
                        "username"=>$username,
                        "password"=>"cbn_lhd_123",
                        "domain"=>"163.com",
                    );
               */
               
               
               $row_other = array(
                        "host"=>"ssl://smtp.gmail.com",
                        "username"=>$username,
                        "password"=>"cbn_lhd_123",
                        "domain"=>"gmail.com",
                    );
               
               return $row_other;
        }
        
        function get_a_yahoo_box($email)
        {
                $md5 = md5($email);
                $suffix = substr($md5, -1, 1);
                
                $arr_0 = array("0","1","2","3","4","5","6","7");
                $arr_1 = array("8","9","a","b","c","d","e","f");
                if (in_array($suffix,$arr_0))
                {
                       $username="cbnweekya";
                }
                else
                {
                       $username="cbnweekyb";
                }
                
               $row_other = array(
                        "host"=>"ssl://smtp.gmail.com",
                        "username"=>$username,
                        "password"=>"cbn_lhd_123",
                        "domain"=>"gmail.com",
                    );
               return $row_other;
        }
        
        function get_a_sohu_box($email)
        {
                $md5 = md5($email);
                $suffix = substr($md5, -1, 1);
                
                $arr_0 = array("0","1","2","3","4","5","6","7");
                $arr_1 = array("8","9","a","b","c","d","e","f");
                if (in_array($suffix,$arr_0))
                {
                       $username="cbnweeksa";
                }
                else
                {
                       $username="cbnweeksb";
                }
                
               $row_other = array(
                        "host"=>"smtp.sina.com",
                        "username"=>$username,
                        "password"=>"cbn_lhd_123",
                        "domain"=>"sina.com",
                    );
               return $row_other;
        }
        
        function get_a_default_box($email)
        {
               $row_default = array(
                        "host"=>"localhost",
                        "username"=>"cbn",
                        "password"=>"cbn123",
                        "domain"=>"cbnweek.net",
                    );
               return $row_default;
        }
        
        function get_a_other_box($email)
        {
                $md5 = md5($email);
                $suffix = substr($md5, -1, 1);
                $suffix = strtolower($suffix);
                //echo $md5."----".$suffix."\n";
                $arr_0 = array("0","1","2","3");
                $arr_1 = array("4","5","6","7");
                $arr_2 = array("8","9","a","b");
                $arr_3 = array("c","d","e","f");
                if (in_array($suffix,$arr_0))
                {
                       $username="cbnweekot";
                }
                else if (in_array($suffix,$arr_1))
                {
                        $username="cbnweekota";
                }
                else if (in_array($suffix,$arr_2))
                {
                        $username="cbnweekha";   
                }
                else
                {
                       $username="cbnweekhb";
                }
                
               $row_other = array(
                        "host"=>"smtp.163.com",
                        "username"=>$username,
                        "password"=>"cbn_lhd_123",
                        "domain"=>"163.com",
                    );
               return $row_other;
        }
}

?>
