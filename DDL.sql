CREATE DATABASE ToDoListDB;
USE ToDoListDB;

CREATE TABLE Task
(
  TaskID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  Description VARCHAR(80),
  Category ENUM('personal', 'school', 'work', 'other') NOT NULL,
  IsFavourite BOOL NOT NULL,
  Status ENUM('toDo', 'inProgress', 'done') NOT NULL,
  Deadline DATETIME NOT NULL,
  CreateDate DATETIME NOT NULL
);