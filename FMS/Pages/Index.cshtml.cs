using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() => LocalRedirect("~/Facilities");
}
