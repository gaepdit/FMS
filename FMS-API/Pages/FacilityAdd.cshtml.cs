using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FMS.DTOs;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;

namespace FMS
{
    public class FacilityAddModel : PageModel
    {
        private readonly FmsDbContext _context;
        public FacilityAddModel(FmsDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // Populate dropdown lists
        }

        public async void OnPost(CreateFacilityDto createFacility)
        {
            // transform Create DTO to EF entity
            Facility newFacility = new Facility { Name = createFacility.Name };
            _context.Facilities.Add(newFacility);
            await _context.SaveChangesAsync();
        }
    }
}