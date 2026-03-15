using System;
using System.Collections.Generic;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.DataStructures;
using TVSchedulingSystem.Database;

namespace TVSchedulingSystem.Services
{
    // Manages schedule operations between the UI, storage structure, and database.
    public class ScheduleManager
    {
        private readonly ScheduleStorage _storage;
        private readonly DatabaseManager _database;

        public ScheduleManager()
        {
            _storage = new ScheduleStorage();
            _database = new DatabaseManager();
        }

        // Loads all schedules from the database into in-memory storage.
        public void LoadSchedulesFromDatabase()
        {
            _storage.Clear();

            List<Schedule> schedules = _database.LoadSchedules();

            foreach (Schedule schedule in schedules)
            {
                _storage.AddSchedule(schedule);
            }
        }

        // Adds a new schedule after validating the duration and calculating the end time.
        public bool AddSchedule(int scheduleId, int channelId, string programId,
            DateTime startTime, int durationMinutes, string imagePath)
        {
            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0);

            DateTime endTime = startTime.AddMinutes(durationMinutes);

            Schedule schedule = new Schedule
            {
                ScheduleID = scheduleId,
                ChannelID = channelId,
                ProgramID = programId,
                StartTime = startTime,
                EndTime = endTime,
                ImagePath = imagePath
            };

            bool added = _storage.AddSchedule(schedule);

            if (added)
            {
                _database.InsertSchedule(schedule);
            }

            return added;
        }

        // Removes a schedule from both storage and database.
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            Schedule? schedule = _storage.GetSchedule(channelId, startTime);

            if (schedule == null)
                return false;

            bool removed = _storage.RemoveSchedule(channelId, startTime);

            if (removed)
            {
                _database.DeleteSchedule(schedule.ScheduleID);
            }

            return removed;
        }

        // Returns all schedules for a given channel.
        public List<Schedule> GetSchedulesByChannel(int channelId)
        {
            return _storage.GetSchedulesByChannel(channelId);
        }

        // Returns one schedule for a given channel and start time.
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            return _storage.GetSchedule(channelId, startTime);
        }

        // Returns all channel IDs currently stored.
        public List<int> GetAllChannels()
        {
            return _storage.GetAllChannels();
        }

        // Clears all in-memory schedule data.
        public void Clear()
        {
            _storage.Clear();
        }
    }
}