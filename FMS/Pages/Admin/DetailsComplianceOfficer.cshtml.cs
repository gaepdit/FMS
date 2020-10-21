using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Admin
{
    public class DetailsComplianceOfficerModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsComplianceOfficerModel(IUserService userService) => _userService = userService;

        public bool Add { get; set; }
        public string Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        //public IList<string> Roles { get; private set; }
        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserAsync(name);
            if (user == null)
            {
                return NotFound();
            }

            Id = user.Id.ToString();
            DisplayName = user.DisplayName;
            Email = user.Email;
            //Roles = await _userService.GetUserRolesAsync(user.Id);

            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            return RedirectToPage("./Index");
        }
    }
}
