using Microsoft.AspNetCore.Identity;

namespace FMS.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool Active { get; set; } = true;

        /// <summary>
        /// A claim that specifies the given name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname
        /// </summary>
        [ProtectedPersonalData]
        [StringLength(150)]
        public string GivenName { get; set; } = string.Empty;

        /// <summary>
        /// A claim that specifies the surname of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname
        /// </summary>
        [ProtectedPersonalData]
        [StringLength(150)]
        public string FamilyName { get; set; } = string.Empty;

        // Auditing properties
        public DateTimeOffset? AccountCreatedAt { get; init; }
        public DateTimeOffset? AccountUpdatedAt { get; set; }
        public DateTimeOffset? ProfileUpdatedAt { get; set; }
        public DateTimeOffset? MostRecentLogin { get; set; }

        public UserProgram UserProgram { get; set; }

        public OrganizationalUnit UserUnit { get; set; }

        public UserPosition UserPosition { get; set; }
    }
}
