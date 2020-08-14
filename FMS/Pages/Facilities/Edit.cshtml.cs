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
        private readonly ILogger<IndexModel> _logger;
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

        // TODO: Restore these after the DTOs are fully built:

        //public IEnumerable<BudgetCode> BudgetCodes { get; private set; }
        //public IEnumerable<EnvironmentalInterest> EnvironmentalInterests { get; set; }
        //public IEnumerable<ComplianceOfficer> ComplianceOfficers { get; set; }
        //public IEnumerable<FacilityType> FacilityTypes { get; set; }
        //public IEnumerable<FacilityStatus> FacilityStatuses { get; set; }
        //public IEnumerable<FileCabinet> FileCabinets { get; set; }
        //public IEnumerable<OrganizationalUnit> OrganizationalUnits { get; set; }

        public EditModel(
            ILogger<IndexModel> logger,
            IFacilityRepository repository,
            FmsDbContext context)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;
            Facility = new FacilityEditDto(await _repository.GetFacilityAsync(id));

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

            // TODO: add await & .ToListAsync() to each of these

            //BudgetCodes = _context.BudgetCodes;
            //EnvironmentalInterests = _context.EnvironmentalInterests;
            //ComplianceOfficers = _context.ComplianceOfficers;
            //FacilityTypes = _context.FacilityTypes;
            //FacilityStatuses = _context.FacilityStatuses;
            //OrganizationalUnits = _context.OrganizationalUnits;
        }
    }
}