using TVSchedulingSystem.Models;
using TVSchedulingSystem.Repositories;

namespace TVSchedulingSystem.Services
{
    public class UserService
    {
        private readonly UserRepository _repo;

        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";

        private const string ManagerUsername = "manager";
        private const string ManagerPassword = "manager123";

        public UserService()
        {
            _repo = new UserRepository();
        }

        // Only Client can register
        public bool Register(string username, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            username = username.Trim();
            password = password.Trim();
            role = role.Trim();

            if (role != "Client")
                return false;

            if (_repo.UsernameExists(username))
                return false;

            User user = new User
            {
                Username = username,
                Password = password,
                Role = role
            };

            _repo.Register(user);
            return true;
        }

        public User Login(string username, string password, string selectedRole)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(selectedRole))
            {
                return null;
            }

            username = username.Trim();
            password = password.Trim();
            selectedRole = selectedRole.Trim();

            // Fixed Admin login
            if (selectedRole == "Admin")
            {
                if (username == AdminUsername && password == AdminPassword)
                {
                    return new User
                    {
                        Username = username,
                        Password = password,
                        Role = "Admin"
                    };
                }

                return null;
            }

            // Fixed Manager login
            if (selectedRole == "Manager")
            {
                if (username == ManagerUsername && password == ManagerPassword)
                {
                    return new User
                    {
                        Username = username,
                        Password = password,
                        Role = "Manager"
                    };
                }

                return null;
            }

            // Client login from database
            if (selectedRole == "Client")
            {
                return _repo.Login(username, password, "Client");
            }

            return null;
        }
    }
}