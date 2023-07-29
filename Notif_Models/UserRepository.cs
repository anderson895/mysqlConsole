using System;
using System.Collections.Generic;
using System.Linq;

namespace Notif_Models
{
    public class UserRepository
    {
        public List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                // The list of users is initially empty in this constructor.
                // You should add users to the list as needed.
            };
        }

        public User GetUserByName(string name)
        {
            // Find and return the user with the given name (case-insensitive search)
#pragma warning disable CS8603 // Possible null reference return.
            return _users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
