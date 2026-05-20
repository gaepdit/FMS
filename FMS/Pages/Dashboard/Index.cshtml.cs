using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Dashboard
{
    public class IndexModel(
        IAuthorizationService authorization, 
        IUserService userService, 
        IFacilityRepository _repository) : PageModel
    {
        public UserView CurrentUser { get; private set; }

        public string UserName {  get; set; }

        public IList<Facility> UserFacilities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity is not { IsAuthenticated: true })
                return Challenge();

            UserName = await userService.GetCurrentUserAsync() is UserView user ? user.DisplayName : "User";

            //UserFacilities = await _repository.GetFacilitiesForUserAsync(user.Id);

            return Page();
        }
    }
}
