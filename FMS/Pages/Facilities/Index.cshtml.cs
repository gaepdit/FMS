using System.Collections.Generic;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using System.Linq;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        private readonly Guid _nullguid = new Guid("00000000-0000-0000-0000-000000000000");

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        public bool ShowResults { get; set; }

        //Guid for Test facility for development
        public Guid TestGuid { get; set; }

        public Facility Facility { get; set; }

        public IEnumerable<Facility> Facilities { get; set; }

        public IEnumerable<County> Counties { get; private set; }

        public IEnumerable<BudgetCode> BudgetCodes { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid guid, bool Active = false, bool Result = false)
        {
            if (guid != _nullguid)
            {
                Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == guid);
                Facilities = new Facility[] { Facility };
            }

            ActiveOnly = Active;
            ShowResults = Result;
            PopulateSelects();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // static data from SeedData for building and testing
            TestGuid = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0");
            // this is where the entity search will go

            //Facilities = _context.Facilities;   //.FirstOrDefaultAsync(m => m.Id == TestGuid);
            //Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == TestGuid);
            ShowResults = true;

            string url = "/Facilities/Index";
            return RedirectToPage(url, new { guid = TestGuid, Active = ActiveOnly, Result = ShowResults });
        }

        private void PopulateSelects()
        {
            Counties = _context.Counties;
            BudgetCodes = _context.BudgetCodes;
        }
    }
}