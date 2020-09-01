using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;

        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool ShowSuccessMessage { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(Guid? id, bool? success)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            Id = user.Id.ToString();
            DisplayName = user.DisplayName;
            Email = user.Email;

            ShowSuccessMessage = success ?? false;
            return Page();
        }
    }
}
