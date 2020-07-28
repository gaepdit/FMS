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
    public class DetailsModel : PageModel
    {
        private readonly FMS.Infrastructure.Contexts.FmsDbContext _context;

        public DetailsModel(FMS.Infrastructure.Contexts.FmsDbContext context)
        {
            _context = context;
        }

        public FacilityDto MyFacility { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyFacility = await _context.Facilities.Where(m => m.Id == id).Select(e => new FacilityDto(e));

            if (MyFacility == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
