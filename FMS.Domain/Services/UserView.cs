using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;

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
            UserProgram = user.UserProgram;
            UserUnit = user.UserUnit;
            UserPosition = user.UserPosition;
        }

        public Guid Id { get; }
        private string GivenName { get; }
        private string FamilyName { get; }
        public string Email { get; }
        public UserProgram UserProgram { get; }
        public OrganizationalUnit UserUnit { get; }
        public UserPosition UserPosition { get; }

        public string DisplayName =>
            string.Join(" ", new[] { GivenName, FamilyName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
