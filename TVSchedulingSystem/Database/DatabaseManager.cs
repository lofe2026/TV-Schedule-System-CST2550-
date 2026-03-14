using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;

using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Database
{
    public class DatabaseManager
    {
        private readonly string _connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=TVSchedulingDB;Trusted_Connection=True;";

        public List<Schedule> LoadSchedules()
        {
            var schedules = new List<Schedule>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT ScheduleID, ChannelID, ProgramID, StartTime, EndTime, ImagePath FROM Schedules";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var schedule = new Schedule
                        {
                            ScheduleID = reader.GetInt32(0),
                            ChannelID = reader.GetInt32(1),
                            ProgramID = reader.GetString(2),
                            StartTime = reader.GetDateTime(3),
                            EndTime = reader.GetDateTime(4),
                            ImagePath = reader.IsDBNull(5) ? "" : reader.GetString(5)
                        };

                        schedules.Add(schedule);
                    }
                }
            }

            return schedules;
        }

        public void InsertSchedule(Schedule schedule)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                    INSERT INTO Schedules (ScheduleID, ChannelID, ProgramID, StartTime, EndTime, ImagePath)
                    VALUES (@ScheduleID, @ChannelID, @ProgramID, @StartTime, @EndTime, @ImagePath)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ScheduleID", schedule.ScheduleID);
                    command.Parameters.AddWithValue("@ChannelID", schedule.ChannelID);
                    command.Parameters.AddWithValue("@ProgramID", schedule.ProgramID);
                    command.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                    command.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                    command.Parameters.AddWithValue("@ImagePath",
                        string.IsNullOrWhiteSpace(schedule.ImagePath) ? DBNull.Value : schedule.ImagePath);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSchedule(int scheduleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Schedules WHERE ScheduleID = @ScheduleID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ScheduleID", scheduleId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}