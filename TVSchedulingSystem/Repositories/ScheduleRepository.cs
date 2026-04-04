using System;
using System.Data.SqlClient;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Data;
using TVSchedulingSystem.DataStructures;

namespace TVSchedulingSystem.Repositories
{
    public class ScheduleRepository
    {
        public void InsertSchedule(Schedule schedule)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query =
                @"INSERT INTO Schedule
                (ScheduleID, ChannelID, ProgramID, StartTime, EndTime, ImagePath)
                VALUES
                (@ScheduleID, @ChannelID, @ProgramID, @StartTime, @EndTime, @ImagePath)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ScheduleID", schedule.ScheduleID);
                    cmd.Parameters.AddWithValue("@ChannelID", schedule.ChannelID);
                    cmd.Parameters.AddWithValue("@ProgramID", schedule.ProgramID);
                    cmd.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                    cmd.Parameters.AddWithValue("@ImagePath",
                        string.IsNullOrWhiteSpace(schedule.ImagePath) ? (object)DBNull.Value : schedule.ImagePath);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSchedule(int channelId, DateTime startTime)
        {
            startTime = new DateTime(
                startTime.Year,
                startTime.Month,
                startTime.Day,
                startTime.Hour,
                startTime.Minute,
                0
            );

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query =
                @"DELETE FROM Schedule
                  WHERE ChannelID = @ChannelID
                  AND StartTime = @StartTime";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ChannelID", channelId);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void LoadSchedules(ScheduleStorage storage)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Schedule";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Schedule schedule = new Schedule
                        {
                            ScheduleID = Convert.ToInt32(reader["ScheduleID"]),
                            ChannelID = Convert.ToInt32(reader["ChannelID"]),
                            ProgramID = reader["ProgramID"].ToString(),
                            StartTime = Convert.ToDateTime(reader["StartTime"]),
                            EndTime = Convert.ToDateTime(reader["EndTime"]),
                            ImagePath = reader["ImagePath"] == DBNull.Value
                                ? string.Empty
                                : reader["ImagePath"].ToString()
                        };

                        storage.AddSchedule(schedule);
                    }
                }
            }
        }
    }
}