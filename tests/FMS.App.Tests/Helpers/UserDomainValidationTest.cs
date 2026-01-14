using FMS.Helpers;

namespace FMS.App.Tests.Helpers
{
    public class UserDomainValidationTests
    {
        [Test]
        public void ShouldValidateEmail()
        {
            const string validEmail = "example@dnr.ga.gov";
            validEmail.IsValidEmailDomain().Should().BeTrue();
        }

        [Test]
        public void ShouldInvalidateEmail()
        {
            const string invalidEmail = "example@gmail.com";
            invalidEmail.IsValidEmailDomain().Should().BeFalse();
        }
    }
}
