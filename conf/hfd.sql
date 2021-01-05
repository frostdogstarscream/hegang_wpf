/*
 Navicat Premium Data Transfer

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 80018
 Source Host           : localhost:3306
 Source Schema         : hfd

 Target Server Type    : MySQL
 Target Server Version : 80018
 File Encoding         : 65001

 Date: 05/01/2021 10:09:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for admin
-- ----------------------------
DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin`  (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `userName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `pwd` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ftsjgd_live
-- ----------------------------
DROP TABLE IF EXISTS `ftsjgd_live`;
CREATE TABLE `ftsjgd_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `GS` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `DD` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `TimeStamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ftsjgdtj
-- ----------------------------
DROP TABLE IF EXISTS `ftsjgdtj`;
CREATE TABLE `ftsjgdtj`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `GS` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `DD` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `TimeStamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ftsjsggy_live
-- ----------------------------
DROP TABLE IF EXISTS `ftsjsggy_live`;
CREATE TABLE `ftsjsggy_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `SD` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '速度',
  `GD` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '高度',
  `GL` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '功率',
  `GLYS` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '功率因素',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ftsjtj
-- ----------------------------
DROP TABLE IF EXISTS `ftsjtj`;
CREATE TABLE `ftsjtj`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TR` int(1) NULL DEFAULT 0 COMMENT '提人',
  `TW` int(1) NULL DEFAULT 0 COMMENT '提物',
  `JX` int(1) NULL DEFAULT 0 COMMENT '检修',
  `JJKC` int(1) NULL DEFAULT 0 COMMENT '紧急开车',
  `XZ` int(1) NULL DEFAULT 0 COMMENT '休止',
  `GZ` int(1) NULL DEFAULT 0 COMMENT '故障',
  `RWTZ` int(1) NULL DEFAULT 0 COMMENT '人为停止',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for ftsjzt_live
-- ----------------------------
DROP TABLE IF EXISTS `ftsjzt_live`;
CREATE TABLE `ftsjzt_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TR` tinyint(1) NULL DEFAULT 0 COMMENT '提人',
  `TW` tinyint(1) NULL DEFAULT 0 COMMENT '提物',
  `JX` tinyint(1) NULL DEFAULT 0 COMMENT '检修',
  `JJKC` tinyint(1) NULL DEFAULT 0 COMMENT '紧急开车',
  `XZ` tinyint(1) NULL DEFAULT 0 COMMENT '休止',
  `GZ` tinyint(1) NULL DEFAULT 0 COMMENT '故障',
  `RWTZ` tinyint(1) NULL DEFAULT 0 COMMENT '人为停止',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fwd
-- ----------------------------
DROP TABLE IF EXISTS `fwd`;
CREATE TABLE `fwd`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1022 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fwd_forecast
-- ----------------------------
DROP TABLE IF EXISTS `fwd_forecast`;
CREATE TABLE `fwd_forecast`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4485 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fwd_h
-- ----------------------------
DROP TABLE IF EXISTS `fwd_h`;
CREATE TABLE `fwd_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fzd
-- ----------------------------
DROP TABLE IF EXISTS `fzd`;
CREATE TABLE `fzd`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1021 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fzd_forecast
-- ----------------------------
DROP TABLE IF EXISTS `fzd_forecast`;
CREATE TABLE `fzd_forecast`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4485 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fzd_h
-- ----------------------------
DROP TABLE IF EXISTS `fzd_h`;
CREATE TABLE `fzd_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fzzyl
-- ----------------------------
DROP TABLE IF EXISTS `fzzyl`;
CREATE TABLE `fzzyl`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `yl` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '主井制动压力',
  `timestamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for fzzyl_h
-- ----------------------------
DROP TABLE IF EXISTS `fzzyl_h`;
CREATE TABLE `fzzyl_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `residual_pressure` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `normal_pressure` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `timestamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for gz
-- ----------------------------
DROP TABLE IF EXISTS `gz`;
CREATE TABLE `gz`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `channel` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `gzname` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mtsjgd_live
-- ----------------------------
DROP TABLE IF EXISTS `mtsjgd_live`;
CREATE TABLE `mtsjgd_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `GS` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `DD` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `TimeStamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mtsjgdtj
-- ----------------------------
DROP TABLE IF EXISTS `mtsjgdtj`;
CREATE TABLE `mtsjgdtj`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `GS` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `DD` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `TimeStamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mtsjsggy_live
-- ----------------------------
DROP TABLE IF EXISTS `mtsjsggy_live`;
CREATE TABLE `mtsjsggy_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `SD` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '速度',
  `GD` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '高度',
  `GL` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '功率',
  `GLYS` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT '0' COMMENT '功率因素',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mtsjtj
-- ----------------------------
DROP TABLE IF EXISTS `mtsjtj`;
CREATE TABLE `mtsjtj`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TM` int(1) NULL DEFAULT 0 COMMENT '提煤',
  `YS` int(1) NULL DEFAULT 0 COMMENT '验绳',
  `JX` int(1) NULL DEFAULT 0 COMMENT '检修',
  `JJKC` int(1) NULL DEFAULT 0 COMMENT '紧急开车',
  `XZ` int(1) NULL DEFAULT 0 COMMENT '休止',
  `GZ` int(1) NULL DEFAULT 0 COMMENT '故障',
  `RWTZ` int(1) NULL DEFAULT 0 COMMENT '人为停止',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mtsjzt_live
-- ----------------------------
DROP TABLE IF EXISTS `mtsjzt_live`;
CREATE TABLE `mtsjzt_live`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TM` tinyint(1) NULL DEFAULT 0 COMMENT '提煤',
  `YS` tinyint(1) NULL DEFAULT 0 COMMENT '验绳',
  `JX` tinyint(1) NULL DEFAULT 0 COMMENT '检修',
  `JJKC` tinyint(1) NULL DEFAULT 0 COMMENT '紧急开车',
  `XZ` tinyint(1) NULL DEFAULT 0 COMMENT '休止',
  `GZ` tinyint(1) NULL DEFAULT 0 COMMENT '故障',
  `RWTZ` tinyint(1) NULL DEFAULT 0 COMMENT '人为停止',
  `TimeStamp` varchar(25) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mwd
-- ----------------------------
DROP TABLE IF EXISTS `mwd`;
CREATE TABLE `mwd`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1022 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mwd_forecast
-- ----------------------------
DROP TABLE IF EXISTS `mwd_forecast`;
CREATE TABLE `mwd_forecast`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4485 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mwd_h
-- ----------------------------
DROP TABLE IF EXISTS `mwd_h`;
CREATE TABLE `mwd_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wd1` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度传感器id',
  `wd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd3` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `wd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '温度',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mzd
-- ----------------------------
DROP TABLE IF EXISTS `mzd`;
CREATE TABLE `mzd`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1021 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mzd_forecast
-- ----------------------------
DROP TABLE IF EXISTS `mzd_forecast`;
CREATE TABLE `mzd_forecast`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4485 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mzd_h
-- ----------------------------
DROP TABLE IF EXISTS `mzd_h`;
CREATE TABLE `mzd_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `zd1` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd2` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0',
  `zd3` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动传感器id',
  `zd4` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '振动',
  `timestamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT '0' COMMENT '时间戳',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mzzyl
-- ----------------------------
DROP TABLE IF EXISTS `mzzyl`;
CREATE TABLE `mzzyl`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `yl` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL COMMENT '副井制动压力',
  `timestamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mzzyl_h
-- ----------------------------
DROP TABLE IF EXISTS `mzzyl_h`;
CREATE TABLE `mzzyl_h`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `residual_pressure` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `normal_pressure` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `timestamp` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `id` int(10) NOT NULL,
  `userName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `pwd` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `age` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `nation` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  `department` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 973480767 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for yyz
-- ----------------------------
DROP TABLE IF EXISTS `yyz`;
CREATE TABLE `yyz`  (
  `Auto_ID` bigint(11) NOT NULL AUTO_INCREMENT,
  `Channel` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `Device` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `ItemName` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  `ItemValue` varbinary(25) NOT NULL,
  `TimeStamp` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
  PRIMARY KEY (`Auto_ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_bin ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
