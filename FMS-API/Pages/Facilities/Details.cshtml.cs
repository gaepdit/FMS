using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        public DetailsModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == id);

            if (Facility == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}