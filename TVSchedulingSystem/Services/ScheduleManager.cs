using System;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.DataStructures;
using TVSchedulingSystem.Repositories;

namespace TVSchedulingSystem.Services
{
    public class ScheduleManager
    {
        private readonly ScheduleStorage _storage;
        private readonly ScheduleRepository _repository;
        private readonly bool _useDatabase;

        public ScheduleManager(bool useDatabase = true)
        {
            _storage = new ScheduleStorage();
            _repository = new ScheduleRepository();
            _useDatabase = useDatabase;
        }

        // Creates a schedule manager that uses the database by default.
        public ScheduleManager() : this(true)
        {
        }

        // ---------------------------------
        // LOAD DATA FROM DATABASE
        // ---------------------------------
        public void LoadFromDatabase()
        {
            _storage.Clear();
            _repository.LoadSchedules(_storage);
        }

        // ---------------------------------
        // ADD SCHEDULE
        // ---------------------------------
        public bool AddSchedule(
            int scheduleId,
            int channelId,
            string programId,
            DateTime startTime,
            int durationMinutes,
            string imagePath)
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

            if (added && _useDatabase)
            {
                _repository.InsertSchedule(schedule);
            }

            return added;
        }

        // Removes a schedule from storage and database.
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            Schedule? schedule = _storage.GetSchedule(channelId, startTime);

            if (schedule == null)
                return false;

            bool removed = _storage.RemoveSchedule(channelId, startTime);

            if (removed && _useDatabase)
            {
                _repository.DeleteSchedule(schedule.ScheduleID);
            }

            return removed;
        }

        // Returns one schedule for a given channel and start time.
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            return _storage.GetSchedule(channelId, startTime);
        }

        // ---------------------------------
        // GET SCHEDULES BY CHANNEL
        // ---------------------------------
        public Schedule[] GetSchedulesByChannel(int channelId)
        {
            return _storage.GetSchedulesByChannel(channelId);
        }

        public void Clear()
        {
            _storage.Clear();
        }
    }
}