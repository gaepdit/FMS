using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Services
{
    /// <summary>
    /// Provide methods for interacting with application user accounts
    /// </summary>
    public interface IUserService
    {
        // Current user
        public Task<ApplicationUser> GetCurrentUserAsync();
        public Task<IList<string>> GetCurrentUserRolesAsync();

        // Any user        
        public Task<bool> UserExistsAsync(Guid id);
        public Task<ApplicationUser> GetUserByIdAsync(Guid id);
        public Task<IList<string>> GetUserRolesAsync(Guid id);
        public Task<IdentityResult> UpdateUserRoleAsync(Guid id, string role, bool addToRole);

        // User search
        public Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter);
    }
}
