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
                @"INSERT INTO Schedules
                (ScheduleID, ChannelID, ProgramID, StartTime, EndTime, ImagePath)
                VALUES
                (@ScheduleID,@ChannelID,@ProgramID,@StartTime,@EndTime,@ImagePath)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ScheduleID", schedule.ScheduleID);
                    cmd.Parameters.AddWithValue("@ChannelID", schedule.ChannelID);
                    cmd.Parameters.AddWithValue("@ProgramID", schedule.ProgramID);
                    cmd.Parameters.AddWithValue("@StartTime", schedule.StartTime);
                    cmd.Parameters.AddWithValue("@EndTime", schedule.EndTime);
                    cmd.Parameters.AddWithValue("@ImagePath", schedule.ImagePath);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // LOAD schedules from SQL into your data structure
        public void LoadSchedules(ScheduleStorage storage)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Schedules";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Schedule schedule = new Schedule
                        {
                            ScheduleID = (int)reader["ScheduleID"],
                            ChannelID = (int)reader["ChannelID"],
                            ProgramID = reader["ProgramID"].ToString(),
                            StartTime = (DateTime)reader["StartTime"],
                            EndTime = (DateTime)reader["EndTime"],
                            ImagePath = reader["ImagePath"].ToString()
                        };

                        storage.AddSchedule(schedule);
                    }
                }
            }
        }
    }
}