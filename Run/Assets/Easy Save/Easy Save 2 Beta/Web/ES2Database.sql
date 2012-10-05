--
-- Table structure for table `easysavedata`
-- 

CREATE TABLE `easysavedata` (
  `savedata` mediumblob NOT NULL,
  `saveid` varchar(10) NOT NULL,
  `tag` varchar(100) NOT NULL,
  PRIMARY KEY  (`saveid`,`tag`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;