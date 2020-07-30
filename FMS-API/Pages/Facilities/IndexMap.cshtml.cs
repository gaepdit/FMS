using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FMS.Pages.Facilities
{
    public class IndexMapModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        [BindProperty]
        public bool ShowMap { get; set; }

        [BindProperty]
        public bool ShowResults { get; set; }

        public IEnumerable<Facility> Facilities { get; private set; }

        public IndexMapModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public void OnGet(bool Map = false)
        {
            ShowMap = Map;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ShowMap = true;

            string url = "/Facilities/IndexMap";
            return RedirectToPage(url, new { Map = ShowMap });
        }
    }
}