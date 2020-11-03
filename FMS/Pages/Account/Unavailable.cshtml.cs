using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class UnavailableModel : PageModel
    {
        public void OnGet()
        {
            // Method intentionally left empty.
        }
    }
}
