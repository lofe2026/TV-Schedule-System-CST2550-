using System.Collections.Generic;
using System.Data.SqlClient;
using TVSchedulingSystem.Data;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Repositories
{
    public class ProgramRepository
    {
        public List<ProgramItem> GetPrograms()
        {
            var list = new List<ProgramItem>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT ProgramCode, ProgramName, ImagePath FROM Programs";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProgramItem
                        {
                            ProgramCode = reader["ProgramCode"].ToString(),
                            ProgramName = reader["ProgramName"].ToString(),
                            ImagePath = reader["ImagePath"] == System.DBNull.Value ? string.Empty : reader["ImagePath"].ToString()
                        };

                        list.Add(item);
                    }
                }
            }

            return list;
        }
    }
}
