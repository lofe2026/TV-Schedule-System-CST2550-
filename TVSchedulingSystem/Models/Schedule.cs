using System;
using System.Collections.Generic;
using System.Text;


namespace TVSchedulingSystem.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int ChannelID { get; set; }
        public string ProgramID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string ImagePath { get; set; }

        public Schedule() { }

        public Schedule(int id, int channelId, string programId, DateTime start, DateTime end)
        {
            ScheduleID = id;
            ChannelID = channelId;
            ProgramID = programId;
            StartTime = start;
            EndTime = end;
        }

    }
}
