using Infrastructure.CommandBase;
using Infrastructure.Result;
using Model;
using System;
using System.Collections.Generic;

namespace GameSalesApi.Features.AccountManagement
{
    /// <summary>
    /// Model for get methods from <see cref="AccountController"/>
    /// </summary>
    public class GetUser : IQuery<Result<IEnumerable<User>>>
    {
        public int UsersMaxCount { get; set; }

        public string Email { get; set; }
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
    }
}
