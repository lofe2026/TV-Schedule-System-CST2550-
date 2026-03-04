using System;
using System.Collections.Generic;
using System.Text;

namespace TVSchedulingSystem.Models
{
    public class Channel
    {
        public int ChannelID { get; set; }
        public string? ChannelName { get; set; }

        public Channel() { }

        public Channel(int id, string name)
        {
            ChannelID = id;
            ChannelName = name;
        }

        public override string ToString()
        {
            return ChannelName ?? string.Empty;
        }
    }
}