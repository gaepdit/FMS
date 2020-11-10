using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            // Method intentionally left empty.
        }
    }
}