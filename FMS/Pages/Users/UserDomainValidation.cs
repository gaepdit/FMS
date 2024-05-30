namespace FMS.Domain.Entities.Users
{
    public static class UserDomainValidation
    {
        public static bool isValidEmailDomain(this string email) =>
            email.EndsWith("@dnr.ga.gov", System.StringComparison.CurrentCultureIgnoreCase);
    }
}