using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        private async Task<ApplicationUser> GetCurrentApplicationUserAsync()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            return principal == null ? null : await _userManager.GetUserAsync(principal);
        }

        public async Task<UserView> GetCurrentUserAsync()
        {
            var user = await GetCurrentApplicationUserAsync();
            return user == null ? null : new UserView(user);
        }

        public async Task<IList<string>> GetCurrentUserRolesAsync() =>
            await _userManager.GetRolesAsync(await GetCurrentApplicationUserAsync());

        // Any user
        public async Task<UserView> GetUserByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user == null ? null : new UserView(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user == null ? null : await _userManager.GetRolesAsync(user);
        }

        private async Task<IdentityResult> UpdateUserRoleAsync(Guid id, string role, bool addToRole)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return IdentityResult.Failed(_userManager.ErrorDescriber.DefaultError());
            }

            var isInRole = await _userManager.IsInRoleAsync(user, role);

            // If (addToRole == isInRole), then no change is needed to the user.
            if (addToRole == isInRole)
            {
                return IdentityResult.Success;
            }

            return addToRole switch
            {
                true => await _userManager.AddToRoleAsync(user, role),
                false => await _userManager.RemoveFromRoleAsync(user, role)
            };
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
        private Task<List<UserView>> GetUsersAsync(string nameFilter, string emailFilter) =>
            _context.Users.AsNoTracking()
                .Where(m => string.IsNullOrEmpty(nameFilter)
                    || m.GivenName.Contains(nameFilter)
                    || m.FamilyName.Contains(nameFilter))
                .Where(m => string.IsNullOrEmpty(emailFilter) || m.Email == emailFilter)
                .OrderBy(m => m.FamilyName).ThenBy(m => m.GivenName)
                .Select(e => new UserView(e))
                .ToListAsync();

        public async Task<List<UserView>> GetUsersAsync(string nameFilter, string emailFilter, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return await GetUsersAsync(nameFilter, emailFilter);
            }

            return (await _userManager.GetUsersInRoleAsync(role))
                .Where(m => string.IsNullOrEmpty(nameFilter)
                    || m.GivenName.Contains(nameFilter)
                    || m.FamilyName.Contains(nameFilter))
                .Where(m => string.IsNullOrEmpty(emailFilter) || m.Email == emailFilter)
                .OrderBy(m => m.FamilyName).ThenBy(m => m.GivenName)
                .Select(e => new UserView(e))
                .ToList();
        }
    }
}