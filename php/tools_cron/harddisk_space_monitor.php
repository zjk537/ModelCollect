<?php

/*
  硬盘空间监控，发邮件
 */
//默认时区为上海
date_default_timezone_set('Asia/Shanghai');
//include_once ('/web/mailadm/conf/all_include_list_conf.php');
include_once 'mail/class.phpmailer.php';
include_once 'mail/class.smtp.php';
include_once 'mail/mail_box_select.php';
include_once 'mail/Mailer.php';
//显示错误
ini_set("display_errors", "On");
error_reporting(E_ALL);
//占用百分比阀值
$warning = 80;
//达到阀值的盘数
$warning_num = 0;
//服务器盘数量
$disk_num = 10;
//不监控盘的序列
$disk_except = array(1, 2);
//smtp服务器地址
$host = 'smtp.163.com';
//使用账户
$account = 'wodeyouxiangwoa';
//使用密码
$password = 'cbn_lhd_123';
//smtp服务器的域
$domain = '163.com';
//创建mailer对象
$mail = new Mailer($host, $account, $password, $domain);
//发送给谁
$toArr = array('liuhongdi@gmail.com', 'wodeyouxiangwoa@163.com');
//发件人用户名
$name = '硬盘空间监控';
//邮件主题
$title = '121.101.219.166服务器';
//获得硬盘使用情况
$command = 'df -h';
$output = array();
exec($command, $output);
//邮件内容
$content = '';
foreach ($output as $k => $v) {
	if (0 == $k) { //第一行 "文件系统              容量  已用  可用 已用%% 挂载点"
		$content = $v;
	} else {
		$line = preg_replace('/[' . chr(32) . ']+/', '#', $v);
		echo $line, "\n";
		$itemArr = explode('#', $line);
		if (isset($itemArr[4]) && intval($itemArr[4]) > $warning) { //监控该盘，同时达到警报阀值
			$warning_num++;
			$title .= ' ' . $itemArr[0] . '已使用' . $itemArr[4] . ' ';
			$content .= '<br /><font color="#FF0000;">' . $v . '</font>';
		} else {
			$content .= '<br />' . $v;
		}
	}
}
$title .= '空间达到阀值警报';
if ($warning_num > 0) {
//	foreach ($toArr as $key => $to) {
//		$mail->send($to, $name, $title, $content);
//	}
	$to = 'liuhongdi@gmail.com,295455159@qq.com';
	$mail->send($to, $name, $title, $content);
}
?>
