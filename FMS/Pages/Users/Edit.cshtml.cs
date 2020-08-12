using FMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FMS.Pages.Users
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
        public Guid Id { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public IEnumerable<IdentityError> Errors { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                //return NotFound();
                user = new Domain.Entities.Users.ApplicationUser()
                {
                    Id = default,
                    Email = "example.one@example.com",
                    GivenName = "Sample",
                    Surname = "User"
                };
            }

            GivenName = user.GivenName;
            Surname = user.Surname;
            Id = id.Value;
            Email = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateUserAsync(Id, GivenName, Surname);
            if (result.Succeeded)
            {
                return RedirectToPage("Details", new { success = true });
            }

            if (!await _userService.UserExistsAsync(Id))
            {
                //return NotFound();
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return Page();
        }
    }
}
