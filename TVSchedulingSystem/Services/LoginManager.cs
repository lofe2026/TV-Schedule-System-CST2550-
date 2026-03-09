using System.Collections.Generic;
using TVSchedulingSystem.Models;

namespace TVSchedulingSystem.Services
{
    public class LoginManager
    {
        private List<User> users;

        public LoginManager()
        {
            users = new List<User>()
            {
                new User("admin","admin123","Admin"),
                new User("manager","manager123","Manager"),
                new User("customer","customer123","Customer")
            };
        }

        public User? Authenticate(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                    return user;
            }

            return null;
        }
    }
}