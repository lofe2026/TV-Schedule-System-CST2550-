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

        public ScheduleManager()
        {
            _storage = new ScheduleStorage();
            _repository = new ScheduleRepository();
        }

        // ---------------------------------
        // LOAD DATA FROM DATABASE
        // ---------------------------------
        public void LoadFromDatabase()
        {
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

            if (added)
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
            return _storage.RemoveSchedule(channelId, startTime);
        }

        // ---------------------------------
        // GET SCHEDULE
        // ---------------------------------
        public Schedule GetSchedule(int channelId, DateTime startTime)
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