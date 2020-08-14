using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly FmsDbContext _context;

        // For Dev
        private readonly Guid _nullguid = new Guid("00000000-0000-0000-0000-000000000000");

        [BindProperty]
        public Guid FormGuid { get; set; }

        [BindProperty]
        public Facility Facility { get; set; }

        [BindProperty]
        public string FileID { get; set; }

        [BindProperty]
        public string FacilityID { get; set; }

        [BindProperty]
        public string FacilityType { get; set; }

        [BindProperty]
        public string FacilityStatus { get; set; }

        [BindProperty]
        public string OrganizationalUnit { get; set; }

        [BindProperty]
        public bool FacilityActive { get; set; } = true;

        [BindProperty]
        public string FacilityName { get; set; }

        [BindProperty]
        public string EnvironnmentalInterest { get; set; }

        [BindProperty]
        public string ComplianceOfficer { get; set; }

        [BindProperty]
        public string BudgetCode { get; set; }

        [BindProperty]
        public string Location { get; set; }

        [BindProperty]
        public string Street { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string County { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty]
        public string ZipCode { get; set; }

        [BindProperty]
        public string Latitude { get; set; }

        [BindProperty]
        public string Longitude { get; set; }

        public IEnumerable<County> Counties { get; private set; }

        public IEnumerable<BudgetCode> BudgetCodes { get; private set; }

        public IEnumerable<EnvironmentalInterest> EnvironmentalInterests { get; set; }

        public IEnumerable<ComplianceOfficer> ComplianceOfficers { get; set; }

        public IEnumerable<FacilityType> FacilityTypes { get; set; }

        public IEnumerable<FacilityStatus> FacilityStatuses { get; set; }

        public IEnumerable<FileCabinet> FileCabinets { get; set; }

        public IEnumerable<OrganizationalUnit> OrganizationalUnits { get; set; }

        public EditModel(
            ILogger<IndexModel> logger,
            FmsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            if (Id != _nullguid)
            {
                Facility = await _context.Facilities.FirstOrDefaultAsync(m => m.Id == Id);
            }

            // set hidden form value for Id
            FormGuid = Id;

            if (Facility == null)
            {
                return NotFound();
            }
            else
            {
                Facility.Id = FormGuid;
                Facility.BudgetCode = new BudgetCode { };
                Facility.ComplianceOfficer = new ComplianceOfficer { };
                Facility.County = new County { };
                Facility.EnvironmentalInterest = new EnvironmentalInterest { };
                Facility.FacilityStatus = new FacilityStatus { };
                Facility.FacilityType = new FacilityType { };
                Facility.File = new File { };
                Facility.OrganizationalUnit = new OrganizationalUnit { };
                Facility.RetentionRecords = new List<RetentionRecord> { };
            }

            PopulateSelects();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // populate Facility values
            // "Name" values will be used to search for correct DTO object
            Facility.Id = FormGuid;
            Facility.BudgetCode = new BudgetCode { Name = BudgetCode };
            Facility.ComplianceOfficer = new ComplianceOfficer { Name = ComplianceOfficer }; 
            Facility.County = new County { Name = County }; 
            Facility.EnvironmentalInterest = new EnvironmentalInterest { Name = EnvironnmentalInterest };
            Facility.OrganizationalUnit = new OrganizationalUnit { Name = OrganizationalUnit };
            Facility.FacilityStatus = new FacilityStatus { Status = "Active" };
            Facility.FacilityType = new FacilityType { Name = FacilityType };
            Facility.File = new File { FileLabel = FileID };
            Facility.FacilityNumber = FacilityID;
            Facility.Name = FacilityName;
            Facility.Location = Location;
            Facility.Address = Street;
            Facility.City = City;
            Facility.State = State;
            Facility.PostalCode = ZipCode;
            Facility.Latitude = Latitude;
            Facility.Longitude = Longitude;
            Facility.RetentionRecords = new List<RetentionRecord> { };

            PopulateSelects();

            //SetSelected();

            string url = "/Facilities/EditConfirm";
            return RedirectToPage(url, Facility);   //new { Facility = Facility }
        }

        private void PopulateSelects()
        {
            Counties = _context.Counties;
            BudgetCodes = _context.BudgetCodes;
            EnvironmentalInterests = _context.EnvironmentalInterests;
            ComplianceOfficers = _context.ComplianceOfficers;
            FacilityTypes = _context.FacilityTypes;
            FacilityStatuses = _context.FacilityStatuses;
            OrganizationalUnits = _context.OrganizationalUnits;
        }
    }
}