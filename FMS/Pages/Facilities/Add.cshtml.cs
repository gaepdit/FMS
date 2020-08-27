using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Facilities
{
    public class AddModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // TODO: Remove _context after moving data access to repositories
        private readonly FmsDbContext _context;

        [BindProperty]
        public FacilityCreateDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList Files { get; set; }
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

        public AddModel(
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
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            await _repository.CreateFacilityAsync(Facility);

            return RedirectToPage("Details", new { id = Id, success = true });
        }
        private async Task PopulateSelectsAsync()
        {
            Files = new SelectList(await _context.Files.ToListAsync(), "Id", "FileLabel");

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