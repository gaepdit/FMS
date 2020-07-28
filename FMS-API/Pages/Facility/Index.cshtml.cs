using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;

namespace FMS.Pages.Facility
{
    public class IndexModel : PageModel
    {
        private readonly FMS.Infrastructure.Contexts.FmsDbContext _context;

        public IndexModel(FMS.Infrastructure.Contexts.FmsDbContext context)
        {
            _context = context;
        }

        public IList<FacilityResultsDto> Facilities { get;set; }
        public IList<County> Counties{ get;set; }

        // Create search form
        public async Task OnGetAsync()
        {
            PopulateSelects();
        }

        // Take search form results and use them to filter database results
        public async IActionResult OnPost(FacilitySearchDto search)
        {
            Facilities = await _context.Facilities
                .Where(e => e.Name = search.Name)
                .ToListAsync();

            PopulateSelects();
            return Page();
        }

        private void PopulateSelects()
        {
            Counties = _context.County.ToListAsync();
        }
    }
}
