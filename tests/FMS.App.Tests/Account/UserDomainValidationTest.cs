using System;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Pages.Account;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NSubstitute;
using Xunit;

namespace FMS.App.Test.Account
{
    public class UserDomainValidationTests
    {
        [Fact]
        public void ShouldValidateEmail(){
            var validEmail = "example@dnr.ga.gov";
            Assert.True(FMS.Helpers.UserDomainValidation.IsValidEmailDomain(validEmail));
        }
        [Fact]
        public void ShouldInvalidateEmail()
        {
            var invalidEmail = "example@gmail.com";
            Assert.False(FMS.Helpers.UserDomainValidation.IsValidEmailDomain(invalidEmail));
        }

    }
}