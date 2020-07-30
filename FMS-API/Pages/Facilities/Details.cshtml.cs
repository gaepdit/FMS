using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string url = "/Facilities/Index";
            return RedirectToPage(url);
        }
    }
}