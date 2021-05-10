using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(
            UserManager<ApplicationUser> userManager,
            FmsDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Current user
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            return principal == null ? null : await _userManager.GetUserAsync(principal);
        }

        public async Task<IList<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetCurrentUserAsync();
            return await _userManager.GetRolesAsync(user);
        }

        // Any user
        public Task<ApplicationUser> GetUserByIdAsync(Guid id) =>
            _userManager.FindByIdAsync(id.ToString());

        public async Task<IList<string>> GetUserRolesAsync(Guid id)
        {
            var user = await GetUserByIdAsync(id);
            return await _userManager.GetRolesAsync(user);
        }

        private async Task<IdentityResult> UpdateUserRoleAsync(Guid id, string role, bool addToRole)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                return IdentityResult.Failed(_userManager.ErrorDescriber.DefaultError());
            }

            var isInRole = await _userManager.IsInRoleAsync(user, role);

            if (addToRole && !isInRole)
            {
                return await _userManager.AddToRoleAsync(user, role);
            }

            if (!addToRole && isInRole)
            {
                return await _userManager.RemoveFromRoleAsync(user, role);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateUserRolesAsync(Guid id, Dictionary<string, bool> roleSettings)
        {
            foreach (var (key, value) in roleSettings)
            {
                var result = await UpdateUserRoleAsync(id, key, value);

                if (result != IdentityResult.Success)
                {
                    return result;
                }
            }

            return IdentityResult.Success;
        }

        // User search
        public Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter) =>
            _context.Users.AsNoTracking()
                .Where(m => string.IsNullOrEmpty(nameFilter)
                    || m.GivenName.Contains(nameFilter)
                    || m.FamilyName.Contains(nameFilter))
                .Where(m => string.IsNullOrEmpty(emailFilter)
                    || m.Email == emailFilter)
                .OrderBy(m => m.FamilyName)
                .ToListAsync();

        public Task<ApplicationUser> GetUserAsync(string email) =>
            _context.Users.AsNoTracking()
                .SingleOrDefaultAsync(m => m.Email == email);
    }
}