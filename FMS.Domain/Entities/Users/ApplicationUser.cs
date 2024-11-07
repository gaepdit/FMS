using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

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
        /// <para><c>oid</c>: The immutable identifier for an object, in this case, a user account. This ID uniquely
        /// identifies the user across applications - two different applications signing in the same user receives the same
        /// value in the <c>oid</c> claim. Microsoft Graph returns this ID as the <c>id</c> property for a user account.
        /// Because the <c>oid</c> allows multiple apps to correlate users, the <c>profile</c> scope is required to receive
        /// this claim. If a single user exists in multiple tenants, the user contains a different object ID in each
        /// tenant - they're considered different accounts, even though the user logs into each account with the same
        /// credentials. The <c>oid</c> claim is a GUID and can't be reused.</para>
        /// <para>Value comes from the <c>ObjectId</c> claim
        /// (<c>"http://schemas.microsoft.com/identity/claims/objectidentifier</c>").</para>
        /// <para>ID token claims reference: https://learn.microsoft.com/en-us/entra/identity-platform/id-token-claims-reference#use-claims-to-reliably-identify-a-user</para>
        /// </summary>
        [PersonalData]
        [StringLength(36)]
        public string ObjectId { get; set; }

        public DateTimeOffset? ProfileUpdatedAt { get; set; }
        public DateTimeOffset? MostRecentLogin { get; set; }
    }
}
