using Infrastructure.CommandBase;
using System;

namespace Model
{
    public class User : IHasDomainEvents<>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NickName { get; set; }
        public string City { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SocialNetworksLink { get; set; }
        public bool SocialNetworksLinkConfirmed { get; set; }
        public Guid? PhotoId { get; set; }
        public bool PhotoConfirmed { get; set; }
        public bool Deleted { get; set; }

        public void GetDomainEvents()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(NewUser newUser)
        {
            Id = !string.IsNullOrEmpty(newUser.Id) ? Guid.Parse(newUser.Id) : Guid.Empty;
            Email = newUser.Email;
            PasswordHash = newUser.PasswordHash;
            Name = newUser.Name;
            Surname = newUser.Surname;
            NickName = !string.IsNullOrEmpty(newUser.NickName) ? newUser.NickName : "id";
            City = newUser.City;
            SocialNetworksLink = newUser.SocialNetworksLink;
            PhotoId = !string.IsNullOrEmpty(newUser.PhotoId) ? Guid.Parse(newUser.PhotoId) : (Guid?)null;
        }
    }
}
