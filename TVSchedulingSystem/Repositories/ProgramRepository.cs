using System;
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

                string query = "SELECT ProgramCode, ProgramName, ImagePath FROM Programs ORDER BY ProgramName";

                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ProgramItem
                        {
                            ProgramCode = reader["ProgramCode"].ToString(),
                            ProgramName = reader["ProgramName"].ToString(),
                            ImagePath = reader["ImagePath"] == DBNull.Value
                                ? string.Empty
                                : reader["ImagePath"].ToString()
                        };

                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public bool AddProgram(string code, string name, string imagePath)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Program code is required.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Program name is required.");

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Programs WHERE ProgramCode = @code";
                using (var checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@code", code.Trim());

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                        return false;
                }

                string insertQuery = @"
                    INSERT INTO Programs (ProgramCode, ProgramName, ImagePath)
                    VALUES (@code, @name, @imagePath)";

                using (var cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@code", code.Trim());
                    cmd.Parameters.AddWithValue("@name", name.Trim());
                    cmd.Parameters.AddWithValue("@imagePath",
                        string.IsNullOrWhiteSpace(imagePath) ? (object)DBNull.Value : imagePath.Trim());

                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }

        public ProgramItem GetProgramByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT ProgramCode, ProgramName, ImagePath FROM Programs WHERE ProgramCode = @code";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@code", code.Trim());

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProgramItem
                            {
                                ProgramCode = reader["ProgramCode"].ToString(),
                                ProgramName = reader["ProgramName"].ToString(),
                                ImagePath = reader["ImagePath"] == DBNull.Value
                                    ? string.Empty
                                    : reader["ImagePath"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}