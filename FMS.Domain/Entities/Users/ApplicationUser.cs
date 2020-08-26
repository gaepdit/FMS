using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [ProtectedPersonalData]
        [StringLength(150)]
        public string GivenName { get; set; }

        [ProtectedPersonalData]
        [StringLength(150)]
        public string FamilyName { get; set; }

        [PersonalData]
        public string SubjectId { get; set; }

        [PersonalData]
        public string ObjectId { get; set; }

        public string DisplayName => string.Join(" ", new string[] { GivenName, FamilyName }.Where(s => !string.IsNullOrEmpty(s)));

        public string SortableFullName => string.Join(", ", new string[] { FamilyName, GivenName }.Where(s => !string.IsNullOrEmpty(s)));
    }
}
