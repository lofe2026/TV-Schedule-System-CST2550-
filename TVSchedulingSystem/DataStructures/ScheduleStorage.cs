using System;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleStorage
    {
        private ScheduleNode[] buckets;
        private int size;

        public ScheduleStorage(int size = 101)
        {
            this.size = size;
            buckets = new ScheduleNode[size];
        }

        // =========================
        // HASH FUNCTION
        // =========================
        private int GetIndex(string key)
        {
            int hash = 0;

            foreach (char c in key)
            {
                hash = (hash * 31 + c) % size;
            }

            return hash;
        }

        // =========================
        // NORMALIZE TIME
        // =========================
        private DateTime NormalizeTime(DateTime time)
        {
            return new DateTime(
                time.Year,
                time.Month,
                time.Day,
                time.Hour,
                time.Minute,
                0
            );
        }

        // =========================
        // CREATE KEY
        // =========================
        private string CreateKey(int channelId, DateTime startTime)
        {
            startTime = NormalizeTime(startTime);
            return channelId + "_" + startTime.ToString("yyyyMMddHHmm");
        }

        // =========================
        // CHECK FOR EXACT KEY
        // =========================
        private bool KeyExists(string key)
        {
            int index = GetIndex(key);
            ScheduleNode current = buckets[index];

            while (current != null)
            {
                if (current.Key == key)
                    return true;

                current = current.Next;
            }

            return false;
        }

        // =========================
        // CHECK CONFLICT
        // =========================
        private bool HasConflict(Schedule schedule)
        {
            for (int i = 0; i < size; i++)
            {
                ScheduleNode current = buckets[i];

                while (current != null)
                {
                    Schedule existing = current.Data;

                    if (existing.ChannelID == schedule.ChannelID &&
                        schedule.StartTime < existing.EndTime &&
                        schedule.EndTime > existing.StartTime)
                    {
                        return true;
                    }

                    current = current.Next;
                }
            }

            return false;
        }

        // =========================
        // ADD SCHEDULE
        // =========================
        public bool AddSchedule(Schedule schedule)
        {
            if (schedule == null)
                throw new ArgumentNullException(nameof(schedule));

            schedule.StartTime = NormalizeTime(schedule.StartTime);
            schedule.EndTime = NormalizeTime(schedule.EndTime);

            if (schedule.EndTime <= schedule.StartTime)
                throw new ArgumentException("End time must be after start time.");

            string key = CreateKey(schedule.ChannelID, schedule.StartTime);

            // Prevent exact duplicate slot
            if (KeyExists(key))
                return false;

            // Prevent overlap on same channel
            if (HasConflict(schedule))
                return false;

            int index = GetIndex(key);

            ScheduleNode newNode = new ScheduleNode(key, schedule);
            newNode.Next = buckets[index];
            buckets[index] = newNode;

            return true;
        }

        // =========================
        // REMOVE
        // =========================
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            string key = CreateKey(channelId, startTime);
            int index = GetIndex(key);

            ScheduleNode current = buckets[index];
            ScheduleNode previous = null;

            while (current != null)
            {
                if (current.Key == key)
                {
                    if (previous == null)
                        buckets[index] = current.Next;
                    else
                        previous.Next = current.Next;

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        // =========================
        // GET ONE SCHEDULE
        // =========================
        public Schedule GetSchedule(int channelId, DateTime startTime)
        {
            string key = CreateKey(channelId, startTime);
            int index = GetIndex(key);

            ScheduleNode current = buckets[index];

            while (current != null)
            {
                if (current.Key == key)
                    return current.Data;

                current = current.Next;
            }

            return null;
        }

        // =========================
        // GET SCHEDULES BY CHANNEL
        // =========================
        public Schedule[] GetSchedulesByChannel(int channelId)
        {
            Schedule[] temp = new Schedule[10];
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                ScheduleNode current = buckets[i];

                while (current != null)
                {
                    if (current.Data.ChannelID == channelId)
                    {
                        if (count >= temp.Length)
                        {
                            temp = ResizeArray(temp);
                        }

                        temp[count] = current.Data;
                        count++;
                    }

                    current = current.Next;
                }
            }

            Schedule[] result = new Schedule[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = temp[i];
            }

            Array.Sort(result, (a, b) => a.StartTime.CompareTo(b.StartTime));

            return result;
        }

        // =========================
        // GET ALL SCHEDULES
        // =========================
        public Schedule[] GetAllSchedules()
        {
            Schedule[] temp = new Schedule[10];
            int count = 0;

            for (int i = 0; i < size; i++)
            {
                ScheduleNode current = buckets[i];

                while (current != null)
                {
                    if (count >= temp.Length)
                    {
                        temp = ResizeArray(temp);
                    }

                    temp[count] = current.Data;
                    count++;
                    current = current.Next;
                }
            }

            Schedule[] result = new Schedule[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = temp[i];
            }

            Array.Sort(result, (a, b) =>
            {
                int channelCompare = a.ChannelID.CompareTo(b.ChannelID);
                if (channelCompare != 0)
                    return channelCompare;

                return a.StartTime.CompareTo(b.StartTime);
            });

            return result;
        }

        // =========================
        // RESIZE TEMP ARRAY
        // =========================
        private Schedule[] ResizeArray(Schedule[] oldArray)
        {
            Schedule[] newArray = new Schedule[oldArray.Length * 2];

            for (int i = 0; i < oldArray.Length; i++)
            {
                newArray[i] = oldArray[i];
            }

            return newArray;
        }

        // =========================
        // CLEAR
        // =========================
        public void Clear()
        {
            buckets = new ScheduleNode[size];
        }
    }
}