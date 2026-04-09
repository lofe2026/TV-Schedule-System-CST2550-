CREATE DATABASE TVSchedulingDB;
GO

USE TVSchedulingDB;
GO

-- PROGRAMS TABLE (NEW)
CREATE TABLE Programs (
    ProgramCode NVARCHAR(10) PRIMARY KEY,
    ProgramName NVARCHAR(100) NOT NULL,
    ImagePath NVARCHAR(255) NOT NULL
);

-- SCHEDULE TABLE (LINKED TO PROGRAMS)
CREATE TABLE Schedule (
    ScheduleID INT PRIMARY KEY,
    ChannelID INT NOT NULL,
    ProgramID NVARCHAR(10) NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    ImagePath NVARCHAR(255),
    CONSTRAINT FK_Schedule_Programs 
        FOREIGN KEY (ProgramID) REFERENCES Programs(ProgramCode)
);

-- USERS TABLE
CREATE TABLE Users (
    UserID INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL
);

INSERT INTO Programs VALUES
('P001', 'Morning News', 'Images/news.jpg'),
('P002', 'Cartoon Hour', 'Images/cartoon.png'),
('P003', 'Movie Time', 'Images/movie.jpeg'),
('P004', 'Sports Live', 'Images/sports.png');

INSERT INTO Schedule VALUES
(1, 1, 'P001', '2026-04-09 08:00:00', '2026-04-09 09:00:00', 'Images/news.jpg'),
(2, 2, 'P002', '2026-04-09 09:00:00', '2026-04-09 10:00:00', 'Images/cartoon.jpg'),
(3, 3, 'P003', '2026-04-09 10:00:00', '2026-04-09 12:00:00', 'Images/movie.jpg');

SELECT * FROM Programs;
SELECT * FROM Schedule;
SELECT * FROM Users;