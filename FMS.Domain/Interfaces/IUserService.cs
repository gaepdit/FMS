using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Interfaces
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetCurrentUserAsync();
        public Task<IdentityResult> UpdateCurrentUserAsync(string givenName, string familyName);
        
        public Task<bool> UserExistsAsync(Guid id);
        public Task<ApplicationUser> GetUserByIdAsync(Guid id);
        public Task<IdentityResult> UpdateUserAsync(Guid id, string givenName, string familyName);

        public Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter);
    }
}
