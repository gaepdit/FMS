﻿using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetCurrentUserAsync();
        public Task<IdentityResult> UpdateCurrentUserAsync(string givenName, string surname);
        public Task<ApplicationUser> GetUserByIdAsync(Guid id);
        public Task<IdentityResult> UpdateUserAsync(Guid id, string givenName, string surname);
        public Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter);
    }
}
