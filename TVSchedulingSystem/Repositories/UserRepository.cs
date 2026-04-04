using System.Data.SqlClient;
using TVSchedulingSystem.Models;
using TVSchedulingSystem.Data;

namespace TVSchedulingSystem.Repositories
{
    public class UserRepository
    {
        public void Register(User user)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Users (Username, Password, Role)
                                 VALUES (@Username, @Password, @Role)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool UsernameExists(string username)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM Users WHERE Username = @Username";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public User Login(string username, string password, string role)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"SELECT * FROM Users
                                 WHERE Username = @Username
                                 AND Password = @Password
                                 AND Role = @Role";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", role);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = (int)reader["UserID"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}