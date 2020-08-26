using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userService.GetCurrentUserAsync()
                // TODO: remove in production
                ?? new ApplicationUser()
                {
                    Id = default,
                    Email = "example.one@example.com",
                    GivenName = "Sample",
                    FamilyName = "User"
                };
            //?? throw new Exception("Current user not found");

            Id = currentUser.Id.ToString();
            DisplayName = currentUser.DisplayName;
            Email = currentUser.Email;

            return Page();
        }
    }
}
