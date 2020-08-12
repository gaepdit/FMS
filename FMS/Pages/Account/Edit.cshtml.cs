using FMS.Domain.Entities.Users;
using FMS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty, Required, StringLength(150), Display(Name = "Given Name")]
        public string GivenName { get; set; }

        [BindProperty, Required, StringLength(150), Display(Name = "Family Name")]
        public string Surname { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var currentUser = await _userService.GetCurrentUserAsync()
                ?? new ApplicationUser()
                {
                    Id = default,
                    Email = "example.one@example.com",
                    GivenName = "Sample",
                    Surname = "User"
                };
            //?? throw new Exception("Current user not found");

            GivenName = currentUser.GivenName;
            Surname = currentUser.Surname;
            Email = currentUser.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateCurrentUserAsync(GivenName, Surname);
            if (result.Succeeded)
            {
                return RedirectToPage("Index", new { success = true });
            }

            return Page();
        }
    }
}
