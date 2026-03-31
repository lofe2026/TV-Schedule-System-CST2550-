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
            if (string.IsNullOrWhiteSpace(programId))
                throw new ArgumentException("Program ID is required.");

            if (durationMinutes <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0
            );

            DateTime endTime = startTime.AddMinutes(durationMinutes);

            Schedule schedule = new Schedule
            {
                ScheduleID = scheduleId,
                ChannelID = channelId,
                ProgramID = programId.Trim(),
                StartTime = startTime,
                EndTime = endTime,
                ImagePath = imagePath ?? string.Empty
            };

            bool added = _storage.AddSchedule(schedule);

            if (added && _useDatabase)
            {
                _repository.InsertSchedule(schedule);
            }

            return added;
        }

        // ---------------------------------
        // REMOVE
        // ---------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0
            );

            bool removed = _storage.RemoveSchedule(channelId, startTime);

            if (removed && _useDatabase)
            {
                _repository.DeleteSchedule(channelId, startTime);
            }

            return removed;
        }

        // ---------------------------------
        // GET SCHEDULE
        // ---------------------------------
        public Schedule GetSchedule(int channelId, DateTime startTime)
        {
            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0
            );

            return _storage.GetSchedule(channelId, startTime);
        }

        // ---------------------------------
        // GET SCHEDULES BY CHANNEL
        // ---------------------------------
        public Schedule[] GetSchedulesByChannel(int channelId)
        {
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
        // CLEAR
        // ---------------------------------
        public void Clear()
        {
            _storage.Clear();
        }
    }
}