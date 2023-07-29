using System;

namespace Notif_Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public User()
        {
            // Constructor to initialize properties with empty strings by default
            Id = string.Empty;
            Name = string.Empty;
        }

        public static implicit operator User(string v)
        {
            throw new NotImplementedException();
        }
    }
}
