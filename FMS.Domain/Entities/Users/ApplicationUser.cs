using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// The URI for a claim that specifies the given name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname
        /// </summary>
        [ProtectedPersonalData]
        [StringLength(150)]
        public string GivenName { get; set; }

        /// <summary>
        /// The URI for a claim that specifies the surname of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname
        /// </summary>
        [ProtectedPersonalData]
        [StringLength(150)]
        public string FamilyName { get; set; }

        /// <summary>
        /// Equivalent to ExternalLoginInfo ProviderKey:
        /// "The unique identifier for this user provided by the login provider."
        /// Also ClaimTypes.NameIdentifier:
        /// "The URI for a claim that specifies the name of an entity, http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier."
        /// </summary>
        [PersonalData]
        public string SubjectId { get; set; }

        /// <summary>
        /// Equivalent to ClaimTypes.ObjectId:
        /// "Old Object Id claim: http://schemas.microsoft.com/identity/claims/objectidentifier."
        /// </summary>
        [PersonalData]
        public string ObjectId { get; set; }

        public string DisplayName =>
            string.Join(" ", new[] {GivenName, FamilyName}.Where(s => !string.IsNullOrEmpty(s)));

        public string SortableFullName =>
            string.Join(", ", new[] {FamilyName, GivenName}.Where(s => !string.IsNullOrEmpty(s)));
    }
}