using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class EditModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        // TODO: Remove _context after moving data access to repositories
        private readonly FmsDbContext _context;

        [BindProperty]
        public FacilityEditDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        // TODO: Move all these properties (as needed) to the Edit DTO:

        //public string FileID { get; set; }
        //public string FacilityID { get; set; }
        //public string FacilityType { get; set; }
        //public string FacilityStatus { get; set; }
        //public string OrganizationalUnit { get; set; }
        //public bool FacilityActive { get; set; } = true;
        //public string FacilityName { get; set; }
        //public string EnvironnmentalInterest { get; set; }
        //public string ComplianceOfficer { get; set; }
        //public string BudgetCode { get; set; }
        //public string Location { get; set; }
        //public string Street { get; set; }
        //public string City { get; set; }
        //public string County { get; set; }
        //public string State { get; set; }
        //public string ZipCode { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }

        public SelectList Counties { get; private set; }
        public SelectList FacilityStatuses { get; private set; }

        // TODO: Restore these after the DTOs are fully built:

        //public SelectList BudgetCodes { get; private set; }
        //public SelectList EnvironmentalInterests { get; private set; }
        //public SelectList FacilityTypes { get; private set; }
        //public SelectList FileCabinets { get; private set; }
        //public SelectList OrganizationalUnits { get; private set; }

        // todo: Add a name property to COs

        //public IEnumerable<ComplianceOfficer> ComplianceOfficers { get; set; }

        public EditModel(
            IFacilityRepository repository,
            FmsDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Facility = new FacilityEditDto(await _repository.GetFacilityAsync(id.Value));

            if (Facility == null)
            {
                return NotFound();
            }

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

            // TODO: If editing facility number, make sure the new number doesn't already exist
            // before trying to save.
            // Alternatively, prohibit editing facility number on this page, and add a separate
            // page to edit facility number.

            try
            {
                await _repository.UpdateFacilityAsync(Id, Facility);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Details", new { id = Id, success = true });
        }

        private async Task PopulateSelectsAsync()
        {
            Counties = new SelectList(await _context.Counties.ToListAsync(), "Id", "Name");

            FacilityStatuses = new SelectList(await _context.FacilityStatuses.ToListAsync(), "Id", "Status");

            //todo: Uncomment as added to DTOs

            //BudgetCodes = new SelectList(await _context.BudgetCodes.ToListAsync(), "Id", "Name");

            //EnvironmentalInterests = new SelectList(await _context.EnvironmentalInterests.ToListAsync(), "Id", "Name");

            //FacilityTypes = new SelectList(await _context.FacilityTypes.ToListAsync(), "Code", "Name");

            //OrganizationalUnits = new SelectList(await _context.OrganizationalUnits.ToListAsync(), "Id", "Name");

            // TODO: add await & .ToListAsync() to COs. 
            // need to get a Name property instead of Empl. Id. to Populate DropDown

            //ComplianceOfficers = _context.ComplianceOfficers;
        }
    }
}