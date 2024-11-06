using FMS.Helpers;
using NUnit.Framework;

namespace FMS.App.Tests.Helpers;

public class UserDomainValidationTests
{
    [Test]
    public void ShouldValidateEmail()
    {
        const string validEmail = "example@dnr.ga.gov";
        Assert.True(validEmail.IsValidEmailDomain());
    }

    [Test]
    public void ShouldInvalidateEmail()
    {
        const string invalidEmail = "example@gmail.com";
        Assert.False(invalidEmail.IsValidEmailDomain());
    }
}
