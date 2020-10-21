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
        public Task<ApplicationUser> GetUserByIdAsync(Guid id);
        public Task<IList<string>> GetUserRolesAsync(Guid id);
        public Task<IdentityResult> UpdateUserRolesAsync(Guid id, Dictionary<string, bool> roleSettings);

        // User search
        public Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter);
        public Task<ApplicationUser> GetUserAsync(string nameFilter);
    }
}
