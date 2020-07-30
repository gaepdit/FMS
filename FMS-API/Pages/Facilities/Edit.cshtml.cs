using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FMS.Pages.Facilities
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        public IEnumerable<Facility> Facilities { get; private set; }

        public IEnumerable<County> Counties { get; private set; }

        public IEnumerable<BudgetCode> BudgetCodes { get; private set; }

        public EditModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            PopulateSelects();
        }
       
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            PopulateSelects();
            string url = "/Facilities/Edit";
            return RedirectToPage(url);
        }
        private void PopulateSelects()
        {
            Counties = _context.Counties;
            BudgetCodes = _context.BudgetCodes;
        }
    }
}