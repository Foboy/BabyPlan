CREATE DATABASE  IF NOT EXISTS `babyplan` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `babyplan`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: babyplan
-- ------------------------------------------------------
-- Server version	5.5.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pro_product_item`
--

DROP TABLE IF EXISTS `pro_product_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pro_product_item` (
  `pro_item_id` int(11) NOT NULL AUTO_INCREMENT,
  `min_age` int(11) NOT NULL DEFAULT '0',
  `max_age` int(11) NOT NULL DEFAULT '0',
  `price` decimal(10,2) NOT NULL DEFAULT '0.00',
  `item_info` varchar(512) DEFAULT NULL,
  `pro_id` int(11) NOT NULL,
  `item_type` int(11) NOT NULL DEFAULT '1' COMMENT '1：衣服 2：玩具 3：其他',
  `create_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `state` int(11) NOT NULL DEFAULT '1' COMMENT '1: 启用 2：禁用',
  `level` int(11) NOT NULL DEFAULT '1' COMMENT '1：普通 2：vip',
  `sex` int(11) NOT NULL DEFAULT '1' COMMENT '1:男 2:女',
  PRIMARY KEY (`pro_item_id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='宝贝详细表';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pro_product_item`
--

LOCK TABLES `pro_product_item` WRITE;
/*!40000 ALTER TABLE `pro_product_item` DISABLE KEYS */;
INSERT INTO `pro_product_item` VALUES (1,7,9,9.00,NULL,1,2,'2012-08-01 12:56:07',0,0,1),(2,4,6,5.00,NULL,2,2,'2012-07-27 20:20:11',0,1,2),(3,7,9,8.00,NULL,2,2,'2012-07-27 20:20:11',0,0,1),(4,43442,43444,8.00,NULL,1,1,'2012-08-01 12:56:07',0,0,1),(5,3,5,4.00,NULL,3,3,'2012-08-01 13:03:35',0,0,1),(9,2,4,2.00,NULL,4,1,'2012-08-20 13:31:06',0,0,1),(10,3,5,3.00,NULL,4,1,'2012-08-20 13:31:07',0,0,1),(11,87,89,88.00,NULL,4,1,'2012-08-20 13:31:07',0,0,1);
/*!40000 ALTER TABLE `pro_product_item` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2012-08-20 22:02:18
