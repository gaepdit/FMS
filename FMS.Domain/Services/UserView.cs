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
        public string GivenName { get; }
        public string FamilyName { get; }
        public string Email { get; }

        [Display(Name = "Program")]
        public UserProgram UserProgram { get; set; }

        [Display(Name = "Unit")]
        public OrganizationalUnit UserUnit { get; set; }

        [Display(Name = "Position")]
        public UserPosition UserPosition { get; set; }

        public string DisplayName =>
            string.Join(" ", new[] { GivenName, FamilyName }.Where(s => !string.IsNullOrEmpty(s)));

        public string Name => string.Join(", ", new[] { FamilyName, GivenName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
