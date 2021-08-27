using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Users
{
    public class IndexModel : PageModel
    {
        public string Name { get; init; }

        [EmailAddress]
        public string Email { get; init; }

        public string Role { get; init; }

        public IEnumerable<SelectListItem> RoleItems { get; } =
            UserRoles.AllRoles.Select(d => new SelectListItem(UserRoles.DisplayName(d), d));

        public bool ShowResults { get; private set; }
        public List<UserView> SearchResults { get; private set; }

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnGetSearchAsync([FromServices] IUserService userService,
            string name, string email, string role)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SearchResults = await userService.GetUsersAsync(name, email, role);
            ShowResults = true;
            return Page();
        }
    }
}