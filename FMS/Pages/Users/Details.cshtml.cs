using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsModel(IUserService userService) => _userService = userService;

        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public DisplayMessage Message { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
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
            Roles = await _userService.GetUserRolesAsync(user.Id);

            Message = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}
