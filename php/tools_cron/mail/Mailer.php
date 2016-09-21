<?php


class Mailer {
	var $mail;
	
	function Mailer($host="ms.cfp.cn" , $username="cfp@cfp.cn" ,$password="cfp123",$domain="cbnweek.net") {

		//$username = 'cfp@cfp.cn';
		//$password = 'cfp12345';
		//$host="ms.cfp.cn";
		$mail = new phpmailer();
		$mail->IsSMTP();     

		$mail->Host = $host; 
		$mail->SMTPAuth = true;
		$mail->Username = $username;
		$mail->Password = $password;
		//$mail->From = "cfp@cfp.cn";
		$mail->From = $username."@".$domain;
		$mail->CharSet = 'utf-8';
		$mail->Encoding = 'base64';
		if ($domain == 'gmail.com')
		{
			//$mail->SMTPSecure = "ssl";
            $mail->Host ='ssl://smtp.gmail.com';  // gmail 要求ssl 所以服务器配置一定ssl
            $mail->Port = 465; // default is 25, gmail is 465 or 587
            $mail->Username = $username."@gmail.com";
	    }
		
		$this->mail = $mail;
    }
    /*
    function addCC($address){
    	$this->mail->AddCC($address);
    }
    
    function addBCC($address){
    	$this->mail->AddBCC($address);
    }

	function setFrom($address){
    	$this->mail->From = $address;
    }

	function AddReplyTo($address){
    	$this->mail->AddReplyTo($address);
    }
    
    function addFile($file){
    	$this->mail->AddAttachment($file);
    }
    */
    
    function send($to,$name,$subject,$message){
		
		//$mailto = split (",", $to);
		$mailto = explode(",", $to);
		foreach($mailto as $thekey=>$oneto ){
			if($oneto!=""){
				$this->mail->AddAddress($oneto);
			}
		}
		
		//$to = trim($to);
		//$this->mail->AddAddress($to);
		$this->mail->FromName = $name;		
		//$this->mail->Subject = "=?GB2312?B?".base64_encode($subject)."?=";
		$this->mail->Subject = "=?UTF-8?B?".base64_encode($subject)."?=";
		//$this->mail->Subject = base64_encode($subject);
		//$this->mail->Body    = str_replace("\n","<br>\n",$message);
		$this->mail->Body    = $message;
		$this->mail->AltBody = $message;
		
		$date = date("Y_m_d");
		$time = date("H-i");
		$log_path = "/web/tools_cron/log/mail_".$date.".txt";
		$log_str = $time."___".$this->mail->From."___".$to."___".$subject;
		if(!$this->mail->Send())
		{
			echo $this->mail->ErrorInfo;
			$log_str.="____".$this->mail->ErrorInfo."\n";
			error_log($log_str, 3, $log_path);
			return -1;
		}
		else
		{
			$log_str.="____succ\n";
			error_log($log_str, 3, $log_path);
		    return 0;
	    }
    }
}
?>
