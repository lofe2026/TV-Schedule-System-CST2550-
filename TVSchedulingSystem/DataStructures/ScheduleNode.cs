using System;
using System.Collections.Generic;
using System.Text;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleNode
    {
        public string Key;
        public Schedule Data;
        public ScheduleNode Next;

        public ScheduleNode(string key, Schedule data)
        {
            Key = key;
            Data = data;
            Next = null;
        }
    }
}