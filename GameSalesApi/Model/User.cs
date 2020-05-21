using System;
using Model.Enums;

namespace Model
{
    public class User 
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

        public void GetDomainEvents()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(NewUser newUser)
        {
            Id = newUser.Id;
            FirstName = newUser.FirstName;
            LastName = newUser.LastName;
            Email = newUser.Email;
            Username = newUser.Username;
            PasswordHash = newUser.PasswordHash;
            PasswordSalt = newUser.PasswordSalt;
            NotificationViaEmail = newUser.NotificationViaEmail;
            NotificationViaTelegram = newUser.NotificationViaTelegram;
            Role = newUser.Role;
            PhotoLink = !string.IsNullOrEmpty(newUser.PhotoLink) ? newUser.PhotoLink : null;
        }
    }
}
