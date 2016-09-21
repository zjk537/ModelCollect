/*
Navicat MySQL Data Transfer

Source Server         : MySql(172.0.0.1)
Source Server Version : 50617
Source Host           : localhost:3306
Source Database       : prize_db

Target Server Type    : MYSQL
Target Server Version : 50617
File Encoding         : 65001

Date: 2015-09-01 12:18:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for sms_logs
-- ----------------------------
DROP TABLE IF EXISTS `sms_logs`;
CREATE TABLE `sms_logs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `phone` varchar(20) NOT NULL COMMENT '目标手机号码',
  `type` varchar(50) NOT NULL COMMENT '短信类型：CBNWeek',
  `msg` varchar(255) NOT NULL COMMENT '短信内容',
  `otime` datetime DEFAULT NULL COMMENT '定时发送时间',
  `result_code` varchar(50) DEFAULT NULL COMMENT '短信发送结果状态码',
  `result_msg` varchar(255) DEFAULT NULL COMMENT '短信发送结果 描述信息',
  `create_date` datetime NOT NULL COMMENT '短信发送时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
