CREATE DATABASE TVSchedulingDB;
GO

USE TVSchedulingDB;
GO

CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY,
    ChannelID INT NOT NULL,
    ProgramID NVARCHAR(50) NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    ImagePath NVARCHAR(255) NULL
);
GO

INSERT INTO Schedules (ScheduleID, ChannelID, ProgramID, StartTime, EndTime, ImagePath)
VALUES
(1, 1, 'P001', '2026-04-01 08:00:00', '2026-04-01 09:00:00', ''),
(2, 1, 'P002', '2026-04-01 09:30:00', '2026-04-01 10:30:00', ''),
(3, 2, 'P003', '2026-04-01 08:00:00', '2026-04-01 09:00:00', '');