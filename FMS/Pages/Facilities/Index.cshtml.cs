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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // TODO: Remove _context after moving data access to repositories
        private readonly FmsDbContext _context;

        //private readonly Guid _nullguid = new Guid("00000000-0000-0000-0000-000000000000");
        [BindProperty]
        public FacilityCreateDto DtoFacility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        [BindProperty]
        public bool ShowResults { get; set; }

        //Guid for Test facility for development
        public Guid TestGuid { get; set; }

        public Facility Facility { get; set; }

        public IEnumerable<Facility> Facilities { get; set; }

        public SelectList Counties { get; private set; }
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }

        // todo: Add a name property to COs
        public SelectList ComplianceOfficers { get; set; }

        // TODO: Restore these after the DTOs are fully built:

        //public SelectList FileCabinets { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
            FmsDbContext context)
        {
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
            // clear the Form Fields
            Facility = null;

            // Change to use Facility DTO instead of domain entity
            // Populate spec from Search fields or add Spec property to this page and bind fields directly to it.
            var spec = new FacilitySpec() { Active = true };
            var FacilityList = await _repository.GetFacilityListAsync(spec);

            ShowResults = true;
            //ActiveOnly = true;

            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            Counties = new SelectList(await _context.Counties.ToListAsync(), "Id", "Name");

            FacilityStatuses = new SelectList(await _context.FacilityStatuses.ToListAsync(), "Id", "Status");

            FacilityTypes = new SelectList(await _context.FacilityTypes.ToListAsync(), "Id", "Name");

            BudgetCodes = new SelectList(await _context.BudgetCodes.ToListAsync(), "Id", "Name");

            OrganizationalUnits = new SelectList(await _context.OrganizationalUnits.ToListAsync(), "Id", "Name");

            EnvironmentalInterests = new SelectList(await _context.EnvironmentalInterests.ToListAsync(), "Id", "Name");

            // TODO: add await & .ToListAsync() to COs. 
            // need to get a Name property instead of Empl. Id. to Populate DropDown
            ComplianceOfficers = new SelectList(await _context.ComplianceOfficers.ToListAsync(), "Id", "Name");
        }
    }
}