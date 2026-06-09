using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Dashboard
{
    public class IndexModel(
        IAuthorizationService authorization, 
        IUserService userService, 
        IDashboardRepository _repository) : PageModel
    {
        public UserView CurrentUser { get; private set; }
        public string UserName {  get; set; }
        public IList<string> Roles { get; private set; }
        public IList<Facility> UserFacilities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity is not { IsAuthenticated: true })
                return Challenge();

            CurrentUser = await userService.GetCurrentUserAsync()
                ?? throw new Exception("Current user not found");

            Roles = await userService.GetCurrentUserRolesAsync();

            UserName = CurrentUser.DisplayName;

            //UserFacilities = await _repository.(CurrentUser.Id);

            return Page();
        }
    }
}
