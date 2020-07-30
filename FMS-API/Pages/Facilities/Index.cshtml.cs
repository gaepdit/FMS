using System.Collections.Generic;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        public Boolean ShowResults { get; set; }

        [BindProperty]
        public Boolean ShowMap { get; set; }

        public IEnumerable<Facility> Facilities { get; private set; }

        public IEnumerable<County> Counties { get; private set; }

        public IEnumerable<BudgetCode> BudgetCodes { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(bool Map = true, bool Result = false)
        {
            ShowMap = Map;
            ShowResults = Result;
            PopulateSelects();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PopulateSelects();
            ShowResults = true;

            string url = "/Facilities/Index";
            return RedirectToPage(url, new { Map = ShowMap, Result = ShowResults });
        }

        private void PopulateSelects()
        {
            Counties = _context.Counties;
            BudgetCodes = _context.BudgetCodes;
        }
    }
}