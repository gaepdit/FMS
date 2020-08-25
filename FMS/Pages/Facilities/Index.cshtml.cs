using System.Collections.Generic;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty]
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IReadOnlyList<FacilitySummaryDto> FacilityList {get; set;}

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        // Shows text results <div> if any results return
        [BindProperty]
        public bool ShowResults { get; set; }

        // Shows if no results in result set
        [BindProperty]
        public bool ShowNone { get; set; }

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
            // Get the list of facilities matching the "Spec" criteria
            FacilityList = await _repository.GetFacilityListAsync(Spec);
            
            // Set "divs" based on search results
            if(FacilityList.Count() > 0)
            {
                ShowResults = true;
                ShowNone = false;
            }
            else
            {
                ShowResults = false;
                ShowNone = true;
            }

            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            // TODO: change to DTOs from direct entity context

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