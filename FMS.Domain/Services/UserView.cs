using System;
using FMS.Domain.Entities.Users;
using System.Linq;

namespace FMS.Domain.Services
{
    public class UserView
    {
        public UserView(ApplicationUser user)
        {
            Id = user.Id;
            GivenName = user.GivenName;
            FamilyName = user.FamilyName;
            Email = user.Email;
        }

        public Guid Id { get; }
        private string GivenName { get; }
        private string FamilyName { get; }
        public string Email { get; }

        public string DisplayName =>
            string.Join(" ", new[] { GivenName, FamilyName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
