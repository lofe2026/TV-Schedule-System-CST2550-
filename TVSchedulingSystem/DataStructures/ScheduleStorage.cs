using System;
using System.Collections.Generic;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.DataStructures
{
    public class ScheduleStorage
    {
        // Root of AVL Tree
        private ScheduleNode root;

        public ScheduleStorage()
        {
            root = null;
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

            if (HasConflict(root, schedule))
                return false;

            root = Insert(root, schedule);

            return true;
        }

        // -------------------------------------
        // AVL Insert
        // -------------------------------------
        private ScheduleNode Insert(ScheduleNode node, Schedule schedule)
        {
            if (node == null)
                return new ScheduleNode(schedule);

            if (schedule.StartTime < node.Data.StartTime)
                node.Left = Insert(node.Left, schedule);
            else
                node.Right = Insert(node.Right, schedule);

            UpdateHeight(node);

            return Balance(node);
        }

        // -------------------------------------
        // Remove Schedule
        // -------------------------------------
        public bool RemoveSchedule(int channelId, DateTime startTime)
        {
            bool removed = false;
            root = Remove(root, channelId, startTime, ref removed);
            return removed;
        }

        private ScheduleNode Remove(
            ScheduleNode node,
            int channelId,
            DateTime startTime,
            ref bool removed)
        {
            if (node == null)
                return null;

            if (startTime < node.Data.StartTime)
            {
                node.Left = Remove(node.Left, channelId, startTime, ref removed);
            }
            else if (startTime > node.Data.StartTime)
            {
                node.Right = Remove(node.Right, channelId, startTime, ref removed);
            }
            else
            {
                if (node.Data.ChannelID == channelId)
                {
                    removed = true;

                    if (node.Left == null)
                        return node.Right;

                    if (node.Right == null)
                        return node.Left;

                    ScheduleNode min = FindMin(node.Right);
                    node.Data = min.Data;
                    node.Right = Remove(node.Right, min.Data.ChannelID, min.Data.StartTime, ref removed);
                }
            }

            UpdateHeight(node);

            return Balance(node);
        }

        // -------------------------------------
        // Find Minimum Node
        // -------------------------------------
        private ScheduleNode FindMin(ScheduleNode node)
        {
            while (node.Left != null)
                node = node.Left;

            return node;
        }

        // -------------------------------------
        // AVL Utilities
        // -------------------------------------

        private int Height(ScheduleNode node)
        {
            return node == null ? 0 : node.Height;
        }

        private void UpdateHeight(ScheduleNode node)
        {
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private int BalanceFactor(ScheduleNode node)
        {
            return Height(node.Left) - Height(node.Right);
        }

        private ScheduleNode Balance(ScheduleNode node)
        {
            int balance = BalanceFactor(node);

            // Left heavy
            if (balance > 1)
            {
                if (BalanceFactor(node.Left) < 0)
                    node.Left = RotateLeft(node.Left);

                return RotateRight(node);
            }

            // Right heavy
            if (balance < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                    node.Right = RotateRight(node.Right);

                return RotateLeft(node);
            }

            return node;
        }

        private ScheduleNode RotateLeft(ScheduleNode x)
        {
            ScheduleNode y = x.Right;
            x.Right = y.Left;
            y.Left = x;

            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }

        private ScheduleNode RotateRight(ScheduleNode y)
        {
            ScheduleNode x = y.Left;
            y.Left = x.Right;
            x.Right = y;

            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        // -------------------------------------
        // Get Schedules By Channel
        // -------------------------------------
        public List<Schedule> GetSchedulesByChannel(int channelId)
        {
            var result = new List<Schedule>();

            TraverseChannel(root, channelId, result);

            return result;
        }

        private void TraverseChannel(ScheduleNode node, int channelId, List<Schedule> list)
        {
            if (node == null)
                return;

            TraverseChannel(node.Left, channelId, list);

            if (node.Data.ChannelID == channelId)
                list.Add(node.Data);

            TraverseChannel(node.Right, channelId, list);
        }

        // -------------------------------------
        // Get Schedule By Start Time
        // -------------------------------------
        public Schedule? GetSchedule(int channelId, DateTime startTime)
        {
            return Search(root, channelId, startTime);
        }

        private Schedule? Search(ScheduleNode node, int channelId, DateTime startTime)
        {
            if (node == null)
                return null;

            if (node.Data.StartTime == startTime && node.Data.ChannelID == channelId)
                return node.Data;

            if (startTime < node.Data.StartTime)
                return Search(node.Left, channelId, startTime);

            return Search(node.Right, channelId, startTime);
        }

        // -------------------------------------
        // Conflict Detection
        // -------------------------------------
        private bool HasConflict(ScheduleNode node, Schedule newSchedule)
        {
            if (node == null)
                return false;

            if (newSchedule.StartTime < node.Data.EndTime &&
                newSchedule.EndTime > node.Data.StartTime)
                return true;

            return HasConflict(node.Left, newSchedule) ||
                   HasConflict(node.Right, newSchedule);
        }

        // -------------------------------------
        // Get All Channels
        // -------------------------------------
        public List<int> GetAllChannels()
        {
            var channels = new List<int>();
            CollectChannels(root, channels);
            return channels;
        }

        private void CollectChannels(ScheduleNode node, List<int> list)
        {
            if (node == null)
                return;

            if (!list.Contains(node.Data.ChannelID))
                list.Add(node.Data.ChannelID);

            CollectChannels(node.Left, list);
            CollectChannels(node.Right, list);
        }

        // -------------------------------------
        // Clear Storage
        // -------------------------------------
        public void Clear()
        {
            root = null;
        }
    }
}