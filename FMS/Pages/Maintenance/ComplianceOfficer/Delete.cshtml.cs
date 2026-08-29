using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ComplianceOfficer
{
    [Authorize(Roles = UserRoles.SiteMaintenance + "," + UserRoles.UserMaintenance)]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }
        public ComplianceOfficerSummaryDto ComplianceOfficer { get; set; }
        public List<FacilityDetailDto> FacilityDetailList { get; set; }
        public bool HasFacilities => FacilityDetailList != null && FacilityDetailList.Count > 0;

        private readonly IComplianceOfficerRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        public DeleteModel(IComplianceOfficerRepository repository, IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;

            ComplianceOfficer = await _repository.GetComplianceOfficerSummaryAsync(Id);

            if (ComplianceOfficer == null)
            {
                return NotFound();
            }

            FacilityDetailList = await _facilityRepository.GetFacilityListForCoDeleteAsync(ComplianceOfficer.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repository.DeleteComplianceOfficerAsync(Id);
            TempData?.SetDisplayMessage(Context.Success, "Compliance Officer successfully deleted.");
            return RedirectToPage("./Index");
        }
    }
}
