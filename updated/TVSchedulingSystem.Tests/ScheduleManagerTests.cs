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
            // 🚀 IMPORTANT: disable database during tests
            _manager = new ScheduleManager(false);
        }

        // -----------------------------------------
        // TEST 1: Add schedule without conflict
        // -----------------------------------------
        [TestMethod]
        public void AddSchedule_NoConflict_ShouldReturnTrue()
        {
            bool result = _manager.AddSchedule(
                1,
                1,
                "News",
                new DateTime(2025, 1, 1, 10, 0, 0),
                60,
                "test.jpg"
            );

            Assert.IsTrue(result);
        }

        // -----------------------------------------
        // TEST 2: Conflict detection
        // -----------------------------------------
        [TestMethod]
        public void AddSchedule_WithConflict_ShouldReturnFalse()
        {
            _manager.AddSchedule(
                1,
                1,
                "News",
                new DateTime(2025, 1, 1, 10, 0, 0),
                60,
                "test.jpg"
            );

            bool result = _manager.AddSchedule(
                2,
                1,
                "Movie",
                new DateTime(2025, 1, 1, 10, 30, 0), // overlap
                60,
                "test.jpg"
            );

            Assert.IsFalse(result);
        }

        // -----------------------------------------
        // TEST 3: Remove schedule
        // -----------------------------------------
        [TestMethod]
        public void RemoveSchedule_ShouldReturnTrue()
        {
            _manager.AddSchedule(
                1,
                1,
                "News",
                new DateTime(2025, 1, 1, 10, 0, 0),
                60,
                "test.jpg"
            );

            bool result = _manager.RemoveSchedule(
                1,
                new DateTime(2025, 1, 1, 10, 0, 0)
            );

            Assert.IsTrue(result);
        }

        // -----------------------------------------
        // TEST 4: Get schedule
        // -----------------------------------------
        [TestMethod]
        public void GetSchedule_ShouldReturnCorrectSchedule()
        {
            DateTime startTime = new DateTime(2025, 1, 1, 10, 0, 0);

            _manager.AddSchedule(
                1,
                1,
                "News",
                startTime,
                60,
                "test.jpg"
            );

            var schedule = _manager.GetSchedule(1, startTime);

            Assert.IsNotNull(schedule);
            Assert.AreEqual("News", schedule.ProgramID);
        }
    }
}