using System;
using System.Collections.Generic;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleStorage
    {
        // ChannelID -> Sorted list of schedules ordered by StartTime
        private readonly Dictionary<int, SortedList<DateTime, Schedule>> _storage;

        public ScheduleStorage()
        {
            _storage = new Dictionary<int, SortedList<DateTime, Schedule>>();
        }

        // -------------------------------------
        // Add Schedule
        // -------------------------------------
        public bool AddSchedule(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            if (schedule.EndTime <= schedule.StartTime)
                throw new ArgumentException("End time must be after start time.");

            // If channel does not exist, create new sorted list
            if (!_storage.ContainsKey(schedule.ChannelID))
            {
                _storage[schedule.ChannelID] =
                    new SortedList<DateTime, Schedule>();
            }

            var channelSchedules = _storage[schedule.ChannelID];

            // Check for time conflict
            if (HasConflict(channelSchedules, schedule))
                return false;

            channelSchedules.Add(schedule.StartTime, schedule);

            return true;
        }

        // -------------------------------------
        // Remove Schedule
        // -------------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            if (!_storage.ContainsKey(channelId))
                return false;

            var channelSchedules = _storage[channelId];



            // 🔥 Normalize startTime before searching
            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0);

            if (!channelSchedules.ContainsKey(startTime))
                return false;

            channelSchedules.Remove(startTime);
            return true;
        }

        // -------------------------------------
        // Get All Schedules For Channel
        // -------------------------------------
        public List<Schedule> GetSchedulesByChannel(int channelId)
        {
            if (!_storage.ContainsKey(channelId))
                return new List<Schedule>();

            return new List<Schedule>(_storage[channelId].Values);
        }

        // -------------------------------------
        // Get Schedule By Exact Start Time
        // -------------------------------------
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            if (!_storage.ContainsKey(channelId))
                return null;

            var channelSchedules = _storage[channelId];

            if (channelSchedules.ContainsKey(startTime))
                return channelSchedules[startTime];

            return null;
        }

        // -------------------------------------
        // Conflict Detection Algorithm
        // -------------------------------------
        private bool HasConflict(SortedList<DateTime, Schedule> schedules, Schedule newSchedule)
        {
            foreach (var existing in schedules.Values)
            {
                if (newSchedule.StartTime < existing.EndTime &&
                    newSchedule.EndTime > existing.StartTime)
                {
                    return true;
                }
            }

            return false;
        }

        // -------------------------------------
        // Get All Channel IDs
        // -------------------------------------


        public List<int> GetAllChannels()
        {
            return new List<int>(_storage.Keys);
        }

        // -------------------------------------
        // Clear Storage
        // -------------------------------------
        public void Clear()
        {
            _storage.Clear();
        }
    }
}