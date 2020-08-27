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

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            if (principal == null)
            {
                return null;
            }

            return await _userManager.GetUserAsync(principal);
        }

        public async Task<IdentityResult> UpdateCurrentUserAsync(string givenName, string familyName)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return IdentityResult.Failed(_userManager.ErrorDescriber.DefaultError());
            }

            user.GivenName = givenName;
            user.FamilyName = familyName;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        //public async Task<IdentityResult> UpdateUserAsync(Guid id, string givenName, string familyName)
        //{
        //    var user = await GetUserByIdAsync(id);
        //    if (user == null)
        //    {
        //        return IdentityResult.Failed(_userManager.ErrorDescriber.DefaultError());
        //    }

        //    user.GivenName = givenName;
        //    user.FamilyName = familyName;

        //    return await _userManager.UpdateAsync(user);
        //}

        public async Task<List<ApplicationUser>> GetUsersAsync(string nameFilter, string emailFilter)
        {
            return await _context.Users.AsNoTracking()
                .Where(m => string.IsNullOrEmpty(nameFilter) || m.GivenName.Contains(nameFilter) || m.FamilyName.Contains(nameFilter))
                .Where(m => string.IsNullOrEmpty(emailFilter) || m.Email == emailFilter)
                .ToListAsync();
        }
    }
}
