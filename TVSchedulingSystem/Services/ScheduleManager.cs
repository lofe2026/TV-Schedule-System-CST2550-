using System;
using System.Collections.Generic;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.DataStructures;

namespace TVSchedulingSystem.Services
{
    public class ScheduleManager
    {
        private readonly ScheduleStorage _storage;

        public ScheduleManager()
        {
            _storage = new ScheduleStorage();
        }


        // Add Schedule
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

            // Normalize time (remove seconds)
            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0);

            DateTime endTime = startTime.AddMinutes(durationMinutes);

            var schedule = new Schedule
            {
                ScheduleID = scheduleId,
                ChannelID = channelId,
                ProgramID = programId,
                StartTime = startTime,
                EndTime = endTime,
                ImagePath = imagePath
            };

            // Send schedule to storage
            return _storage.AddSchedule(schedule);
        }

        // Remove Schedule
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            return _storage.RemoveSchedule(channelId, startTime);
        }

        // Get Schedules By Channel

        public List<Schedule> GetSchedulesByChannel(int channelId)
        {
            return _storage.GetSchedulesByChannel(channelId);
        }

        // Get Specific Schedule
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            return _storage.GetSchedule(channelId, startTime);
        }

        // Get All Channel IDs
        public List<int> GetAllChannels()
        {
            return _storage.GetAllChannels();
        }

        // Clear All Data
        public void Clear()
        {
            _storage.Clear();
        }
    }
}
