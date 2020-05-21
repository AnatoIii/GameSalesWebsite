using System;
using Model.Enums;

namespace Model
{
    public class NewUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool NotificationViaEmail { get; set; }
        public bool NotificationViaTelegram { get; set; }
        public UserRole Role { get; set; }
        public string PhotoLink { get; set; }
    }
}
