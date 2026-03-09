using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TVSchedulingSystem.Services;

namespace TVSchedulingSystem.Tests
{
    [TestClass]
    public class ScheduleManagerTests
    {
        private ScheduleManager _manager;

        [TestInitialize]
        public void Setup()
        {
            _manager = new ScheduleManager();
        }

        // -------------------------------------------------
        // TEST 1: Add schedule without conflict
        // -------------------------------------------------
        [TestMethod]
        public void AddSchedule_NoConflict_ShouldReturnTrue()
        {
            var result = _manager.AddSchedule(
                scheduleId: 1,
                channelId: 1,
                programId: 100,
                startTime: new DateTime(2025, 1, 1, 10, 0, 0),
                durationMinutes: 60);

            Assert.IsTrue(result);
        }

        // -------------------------------------------------
        // TEST 2: Add schedule with conflict
        // -------------------------------------------------
        [TestMethod]
        public void AddSchedule_WithConflict_ShouldReturnFalse()
        {
            _manager.AddSchedule(
                1, 1, 100,
                new DateTime(2025, 1, 1, 10, 0, 0),
                60);

            var result = _manager.AddSchedule(
                2, 1, 101,
                new DateTime(2025, 1, 1, 10, 30, 0), // Overlaps
                60);

            Assert.IsFalse(result);
        }

        // -------------------------------------------------
        // TEST 3: Remove schedule
        // -------------------------------------------------
        [TestMethod]
        public void RemoveSchedule_ShouldReturnTrue()
        {
            _manager.AddSchedule(
                1, 1, 100,
                new DateTime(2025, 1, 1, 10, 0, 0),
                60);

            var result = _manager.RemoveSchedule(
                1,
                new DateTime(2025, 1, 1, 10, 0, 0));

            Assert.IsTrue(result);
        }

        // -------------------------------------------------
        // TEST 4: Get schedule by time
        // -------------------------------------------------
        [TestMethod]
        public void GetSchedule_ShouldReturnCorrectSchedule()
        {
            var startTime = new DateTime(2025, 1, 1, 10, 0, 0);

            _manager.AddSchedule(
                1, 1, 100,
                startTime,
                60);

            var schedule = _manager.GetSchedule(1, startTime);

            Assert.IsNotNull(schedule);
            Assert.AreEqual(100, schedule.ProgramID);
        }
    }
}