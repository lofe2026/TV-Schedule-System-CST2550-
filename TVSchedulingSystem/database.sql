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
('P001', 'Breaking News', 'Images/breakingNews.jpg'),
('P002', 'Cunk on Earth', 'Images/cunkOnEarth.jpg'),
('P003', 'Doctor Who', 'Images/doctorWho.jpg'),
('P004', 'Friends', 'Images/friends.jpg'),
('P005', 'Inception', 'Images/inception.jpg'),
('P006', 'Interstellar', 'Images/interstellar.jpg'),
('P007', 'Jimmy Kimmel Live!', 'Images/jimmyKimmelLive.jpg'),
('P008', 'Morning News', 'Images/theMorningNews.jpg'),
('P009', 'Mr. Bean', 'Images/mrBean.jpg'),
('P010', 'Night News', 'Images/nightNews.jpg'),
('P011', 'The Proposal', 'Images/theProposal.jpg'),
('P012', 'The Ugly Truth', 'Images/theUglyTruth.jpg'),
('P013', 'Top Gear', 'Images/topGear.jpg'),
('P014', 'Weather Forecast', 'Images/weatherForecast.jpg');

INSERT INTO Schedule VALUES
(1, 1, 'P004', '2026-04-09 08:00:00', '2026-04-09 09:00:00', 'Images/friends.jpg'),
(2, 2, 'P007', '2026-04-09 20:00:00', '2026-04-09 21:00:00', 'Images/jimmyKimmelLive.jpg'),
(3, 3, 'P013', '2026-04-09 10:00:00', '2026-04-09 12:00:00', 'Images/topGear.jpg');