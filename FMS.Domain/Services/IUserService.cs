using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FMS.Domain.Services
{
    /// <summary>
    /// Provide methods for interacting with application user accounts
    /// </summary>
    public interface IUserService
    {
        // Current user
        public Task<UserView> GetCurrentUserAsync();
        public Task<IList<string>> GetCurrentUserRolesAsync();

        // Any user        
        public Task<UserView> GetUserByIdAsync(Guid id);
        public Task<IList<string>> GetUserRolesAsync(Guid id);
        public Task<IdentityResult> UpdateUserRolesAsync(Guid id, Dictionary<string, bool> roleSettings);

        // User search
        public Task<List<UserView>> GetUsersAsync(string nameFilter, string emailFilter, string role);
    }
}