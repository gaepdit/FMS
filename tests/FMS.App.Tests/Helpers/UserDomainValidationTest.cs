using FMS.Helpers;
using Xunit;

namespace FMS.App.Tests.Helpers;

public class UserDomainValidationTests
{
    [Fact]
    public void ShouldValidateEmail()
    {
        const string validEmail = "example@dnr.ga.gov";
        Assert.True(validEmail.IsValidEmailDomain());
    }

    [Fact]
    public void ShouldInvalidateEmail()
    {
        const string invalidEmail = "example@gmail.com";
        Assert.False(invalidEmail.IsValidEmailDomain());
    }
}
