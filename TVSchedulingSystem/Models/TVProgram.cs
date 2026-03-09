using System;
using System.Collections.Generic;
using System.Text;

namespace TVSchedulingSystem.Models
{
    public class TVProgram
    {
        public int ProgramID { get; set; }
        public string? Title { get; set; }
        public int DurationMinutes { get; set; }

        public TVProgram() { }

        public TVProgram(int id, string title, int duration)
        {
            ProgramID = id;
            Title = title;
            DurationMinutes = duration;
        }

        public override string ToString()
        {
            return Title ?? string.Empty;
        }
    }
}