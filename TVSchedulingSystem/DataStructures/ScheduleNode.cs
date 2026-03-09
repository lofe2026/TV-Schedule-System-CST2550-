using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleNode
    {
        public Schedule Data;
        public ScheduleNode? Left;
        public ScheduleNode? Right;
        public int Height;

        public ScheduleNode(Schedule schedule)
        {
            Data = schedule;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}