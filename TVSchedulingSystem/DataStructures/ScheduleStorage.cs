using System;
using TVSchedulingSystem.Models;
using System.Data.SqlClient;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleStorage
    {
        private Schedule[] schedules;
        private int count;

        public ScheduleStorage()
        {
            schedules = new Schedule[100];
            count = 0;
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

            // Conflict detection
            for (int i = 0; i < count; i++)
            {
                var s = schedules[i];

                if (s.ChannelID == schedule.ChannelID &&
                    schedule.StartTime < s.EndTime &&
                    schedule.EndTime > s.StartTime)
                {
                    return false;
                }
            }

            if (count >= schedules.Length)
                Resize();

            schedules[count++] = schedule;

            return true;
        }

        // -------------------------------------
        // Resize Array
        // -------------------------------------
        private void Resize()
        {
            Schedule[] newArray = new Schedule[schedules.Length * 2];

            for (int i = 0; i < schedules.Length; i++)
                newArray[i] = schedules[i];

            schedules = newArray;
        }

        // -------------------------------------
        // Remove Schedule
        // -------------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            for (int i = 0; i < count; i++)
            {
                if (schedules[i].ChannelID == channelId &&
                    schedules[i].StartTime == startTime)
                {
                    for (int j = i; j < count - 1; j++)
                        schedules[j] = schedules[j + 1];

                    count--;
                    return true;
                }
            }

            return false;
        }

        // -------------------------------------
        // Get Schedule
        // -------------------------------------
        public Schedule GetSchedule(int channelId, DateTime startTime)
        {
            for (int i = 0; i < count; i++)
            {
                if (schedules[i].ChannelID == channelId &&
                    schedules[i].StartTime == startTime)
                {
                    return schedules[i];
                }
            }

            return null;
        }

        // -------------------------------------
        // Get schedules by channel
        // -------------------------------------
        public Schedule[] GetSchedulesByChannel(int channelId)
        {
            Schedule[] result = new Schedule[count];
            int index = 0;

            for (int i = 0; i < count; i++)
            {
                if (schedules[i].ChannelID == channelId)
                {
                    result[index++] = schedules[i];
                }
            }

            Schedule[] final = new Schedule[index];

            for (int i = 0; i < index; i++)
                final[i] = result[i];

            return final;
        }

        // -------------------------------------
        // Get all channels
        // -------------------------------------
        public int[] GetAllChannels()
        {
            int[] channels = new int[count];
            int index = 0;

            for (int i = 0; i < count; i++)
            {
                bool exists = false;

                for (int j = 0; j < index; j++)
                {
                    if (channels[j] == schedules[i].ChannelID)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                    channels[index++] = schedules[i].ChannelID;
            }

            int[] final = new int[index];

            for (int i = 0; i < index; i++)
                final[i] = channels[i];

            return final;
        }

        // -------------------------------------
        // Clear
        // -------------------------------------
        public void Clear()
        {
            schedules = new Schedule[100];
            count = 0;
        }
    }
}