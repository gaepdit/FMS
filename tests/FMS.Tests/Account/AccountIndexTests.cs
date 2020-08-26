﻿using FluentAssertions;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Threading.Tasks;
using TestHelpers;
using Xunit;

namespace FMS.Tests.Account
{
    public class AccountIndexTests
    {
        private static readonly ApplicationUser _user = new ApplicationUser()
        {
            Id = default,
            Email = "example.one@example.com",
            GivenName = "Sample",
            FamilyName = "User"
        };

        [Fact]
        public async Task OnGet_PopulatesThePageModel()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(l => l.GetCurrentUserAsync())
                .ReturnsAsync(_user)
                .Verifiable();
            var pageModel = new Pages.Account.IndexModel(mockUserService.Object);

            var result = await pageModel.OnGetAsync().ConfigureAwait(false);

            result.Should().BeOfType<PageResult>();
            pageModel.Id.Should().Be(_user.Id.ToString());
            pageModel.Email.Should().Be(_user.Email);
            pageModel.DisplayName.Should().Be(_user.DisplayName);
        }
    }
}
