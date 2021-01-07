CREATE DATABASE IF NOT EXISTS tenQuestionsGame;
USE tenQuestionsGame;

CREATE TABLE `answers` (
  `questionID` int NOT NULL,
  `answerID` int DEFAULT NULL,
  PRIMARY KEY (`questionID`)
);

CREATE TABLE `choices` (
  `ID` int DEFAULT NULL,
  `choiceID` int DEFAULT NULL,
  `choice` char(50) DEFAULT NULL
);

CREATE TABLE `questions` (
  `ID` int NOT NULL,
  `question` char(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
);

CREATE TABLE `users` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `userName` char(30) DEFAULT NULL,
  `score` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
);


