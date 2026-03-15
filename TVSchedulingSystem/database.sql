CREATE DATABASE TVSchedulingDB;
GO

USE TVSchedulingDB;

CREATE TABLE Schedule (
    ScheduleID INT PRIMARY KEY,
    ChannelID INT,
    ProgramID NVARCHAR(100),
    StartTime DATETIME,
    EndTime DATETIME,
    ImagePath NVARCHAR(255)
);


