using System.Security.Claims;

namespace FMS.App
{
    /// <summary>
    /// Constants for claim types.
    /// See https://github.com/AzureAD/microsoft-identity-web/blob/efe2a4069267c8a2479fb3aa2d8d52a974691e22/src/Microsoft.Identity.Web/Constants/ClaimConstants.cs
    /// </summary>
    public static class ClaimConstants
    {
        /// <summary>
        /// Old Object Id claim: http://schemas.microsoft.com/identity/claims/objectidentifier.
        /// </summary>
        public const string ObjectId = "http://schemas.microsoft.com/identity/claims/objectidentifier";

        /// <summary>
        /// PreferredUserName: "preferred_username".
        /// </summary>
        public const string PreferredUserName = "preferred_username";

        /// <summary>
        /// FamilyName: System.Security.Claims.ClaimTypes.Surname: 
        /// "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"
        /// </summary>
        public const string FamilyName = ClaimTypes.Surname;
    }
}
