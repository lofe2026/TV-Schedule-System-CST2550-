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

        public ScheduleManager() : this(true)
        {
        }

        public ScheduleManager(bool useDatabase)
        {
            _useDatabase = useDatabase;
            _storage = new ScheduleStorage();
            _repository = new ScheduleRepository();

            if (_useDatabase)
            {
                LoadFromDatabase();
            }
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
            ValidateScheduleInput(scheduleId, channelId, programId, startTime, durationMinutes);

            DateTime normalizedStartTime = NormalizeToMinute(startTime);
            DateTime endTime = normalizedStartTime.AddMinutes(durationMinutes);

            Schedule schedule = new Schedule
            {
                ScheduleID = scheduleId,
                ChannelID = channelId,
                ProgramID = programId.Trim(),
                StartTime = normalizedStartTime,
                EndTime = endTime,
                ImagePath = string.IsNullOrWhiteSpace(imagePath) ? string.Empty : imagePath.Trim()
            };

            bool added = _storage.AddSchedule(schedule);

            if (added && _useDatabase)
            {
                _repository.InsertSchedule(schedule);
            }

            return added;
        }

        // ---------------------------------
        // REMOVE SCHEDULE
        // ---------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            if (channelId <= 0)
                throw new ArgumentException("Channel ID must be greater than zero.");

            DateTime normalizedStartTime = NormalizeToMinute(startTime);

            bool removed = _storage.RemoveSchedule(channelId, normalizedStartTime);

            if (removed && _useDatabase)
            {
                _repository.DeleteSchedule(channelId, normalizedStartTime);
            }

            return removed;
        }

        // ---------------------------------
        // GET SINGLE SCHEDULE
        // ---------------------------------
        public Schedule GetSchedule(int channelId, DateTime startTime)
        {
            if (channelId <= 0)
                throw new ArgumentException("Channel ID must be greater than zero.");

            DateTime normalizedStartTime = NormalizeToMinute(startTime);
            return _storage.GetSchedule(channelId, normalizedStartTime);
        }

        // ---------------------------------
        // GET SCHEDULES BY CHANNEL
        // ---------------------------------
        public Schedule[] GetSchedulesByChannel(int channelId)
        {
            if (channelId <= 0)
                throw new ArgumentException("Channel ID must be greater than zero.");

            return _storage.GetSchedulesByChannel(channelId);
        }

        // ---------------------------------
        // GET ALL SCHEDULES
        // ---------------------------------
        public Schedule[] GetAllSchedules()
        {
            return _storage.GetAllSchedules();
        }

        // ---------------------------------
        // CLEAR STORAGE ONLY
        // ---------------------------------
        public void Clear()
        {
            _storage.Clear();
        }

        // ---------------------------------
        // VALIDATION
        // ---------------------------------
        private void ValidateScheduleInput(
            int scheduleId,
            int channelId,
            string programId,
            DateTime startTime,
            int durationMinutes)
        {
            if (scheduleId <= 0)
                throw new ArgumentException("Schedule ID must be greater than zero.");

            if (channelId <= 0)
                throw new ArgumentException("Channel ID must be greater than zero.");

            if (string.IsNullOrWhiteSpace(programId))
                throw new ArgumentException("Program name is required.");

            programId = programId.Trim();

            if (programId.Length < 2)
                throw new ArgumentException("Program name must be at least 2 characters long.");

            if (programId.Length > 100)
                throw new ArgumentException("Program name cannot exceed 100 characters.");

            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            if (durationMinutes > 600)
                throw new ArgumentException("Duration cannot exceed 600 minutes.");

            if (startTime == DateTime.MinValue)
                throw new ArgumentException("A valid start time is required.");
        }

        // ---------------------------------
        // NORMALIZE TIME
        // ---------------------------------
        private DateTime NormalizeToMinute(DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                0
            );
        }
    }
}