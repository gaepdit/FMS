using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        [TempData]
        public bool LoggedOut { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager) => _signInManager = signInManager;

        public IActionResult OnGet() => LocalRedirect("~/Account");

        public async Task<IActionResult> OnPostAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                await _signInManager.SignOutAsync();
                LoggedOut = true;
            }

            return LocalRedirect("~/");
        }
    }
}
