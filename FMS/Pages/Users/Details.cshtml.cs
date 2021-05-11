using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Platform.Extensions;

namespace FMS.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsModel(IUserService userService) => _userService = userService;

        public string Id { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public IList<string> Roles { get; private set; }
        public DisplayMessage Message { get; private set; }

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