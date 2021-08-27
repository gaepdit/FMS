using System;
using FMS.Domain.Entities.Users;

namespace FMS.Domain.Services
{
    public class UserView
    {
        public UserView(ApplicationUser user) =>
            (Id, DisplayName, Email) = (user.Id, user.DisplayName, user.Email);

        public Guid Id { get; }
        public string DisplayName { get; }
        public string Email { get; }
    }
}