using System;
using System.Collections.Generic;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleStorage
    {
        // Hash Table: ChannelID → List of schedules
        private Dictionary<int, List<Schedule>> table;

        public ScheduleStorage()
        {
            table = new Dictionary<int, List<Schedule>>();
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

            // Create channel bucket if it does not exist
            if (!table.ContainsKey(schedule.ChannelID))
                table[schedule.ChannelID] = new List<Schedule>();

            var schedules = table[schedule.ChannelID];

            // Conflict detection
            foreach (var s in schedules)
            {
                if (schedule.StartTime < s.EndTime &&
                    schedule.EndTime > s.StartTime)
                {
                    return false;
                }
            }

            schedules.Add(schedule);
            return true;
        }

        // -------------------------------------
        // Remove Schedule
        // -------------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            if (!table.ContainsKey(channelId))
                return false;

            var schedules = table[channelId];

            for (int i = 0; i < schedules.Count; i++)
            {
                if (schedules[i].StartTime == startTime)
                {
                    schedules.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        // -------------------------------------
        // Get Schedule By Start Time
        // -------------------------------------
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            if (!table.ContainsKey(channelId))
                return null;

            foreach (var schedule in table[channelId])
            {
                if (schedule.StartTime == startTime)
                    return schedule;
            }

            return null;
        }

        // -------------------------------------
        // Get Schedules By Channel
        // -------------------------------------
        public List<Schedule> GetSchedulesByChannel(int channelId)
        {
            if (!table.ContainsKey(channelId))
                return new List<Schedule>();

            return new List<Schedule>(table[channelId]);
        }

        // -------------------------------------
        // Get All Channels
        // -------------------------------------
        public List<int> GetAllChannels()
        {
            return new List<int>(table.Keys);
        }

        // -------------------------------------
        // Clear Storage
        // -------------------------------------
        public void Clear()
        {
            table.Clear();
        }
    }
}
