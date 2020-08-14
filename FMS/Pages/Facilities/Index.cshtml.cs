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
using FMS.Domain.Repositories;
using FMS.Domain.Dto;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IFacilityRepository _repository;

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
            IFacilityRepository repository,
            FmsDbContext context)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // static data from SeedData for building and testing
            TestGuid = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0");
            // this is where the entity search will go

            Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == TestGuid);
            Facilities = new List<Facility> { Facility };

            // Change to use Facility DTO instead of domain entity
            // Populate spec from Search fields or add Spec property to this page and bind fields directly to it.
            var spec = new FacilitySpec() { Active = true };
            var FacilityList = await _repository.GetFacilityListAsync(spec);

            ShowResults = true;
            ActiveOnly = true;

            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            Counties = await _context.Counties.ToListAsync();
            BudgetCodes = await _context.BudgetCodes.ToListAsync();
        }
    }
}