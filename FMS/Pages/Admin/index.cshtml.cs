using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FMS.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        [BindProperty]
        public IEnumerable<BudgetCode> BudgetCodes { get; private set; }

        [BindProperty]
        public IEnumerable<FileCabinet> FileCabinets { get; set; }

       
        public IndexModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            PopulateListBoxes();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string url = "/Admin/Index";
            return RedirectToPage(url);
        }

        private void PopulateListBoxes()
        {
            FileCabinets = _context.FileCabinets;
            BudgetCodes = _context.BudgetCodes;
        }
    }
}