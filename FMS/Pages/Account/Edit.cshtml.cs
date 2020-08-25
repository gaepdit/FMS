//using FMS.Domain.Entities.Users;
//using FMS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    public class EditModel : PageModel
    {
        // All user info comes from SOG work account, so no editing needed
        public IActionResult OnGet() => LocalRedirect("~/Account");

        //private readonly IUserService _userService;

        //public EditModel(IUserService userService)
        //{
        //    _userService = userService;
        //}

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required, EmailAddress]
        //    public string Email { get; set; }

        //    [Required, StringLength(150), Display(Name = "Given Name")]
        //    public string GivenName { get; set; }

        //    [Required, StringLength(150), Display(Name = "Family Name")]
        //    public string FamilyName { get; set; }
        //}

        //    public async Task<IActionResult> OnGet()
        //    {
        //        var currentUser = await _userService.GetCurrentUserAsync()
        //            // TODO: remove in production
        //            ?? new ApplicationUser()
        //            {
        //                Id = default,
        //                Email = "example.one@example.com",
        //                GivenName = "Sample",
        //                FamilyName = "User"
        //            };
        //        //?? throw new Exception("Current user not found");

        //        Input = new InputModel
        //        {
        //            GivenName = currentUser.GivenName,
        //            FamilyName = currentUser.FamilyName,
        //            Email = currentUser.Email
        //        };

        //        return Page();
        //    }

        //    public async Task<IActionResult> OnPostAsync()
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return Page();
        //        }

        //        var result = await _userService.UpdateCurrentUserAsync(Input.GivenName, Input.FamilyName);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToPage("./Index", new { success = true });
        //        }

        //        foreach (var err in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, err.Description);
        //        }

        //        return Page();
        //    }
        //}
    }
}