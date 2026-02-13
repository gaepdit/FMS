using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Status
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IStatusRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IStatusRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public StatusEditDto EditStatus { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList SourceStatuses { get; private set; }
        public SelectList SoilStatuses { get; private set; }
        public SelectList GroundwaterStatuses { get; private set; }
        public SelectList OverallStatuses { get; private set; }
        public SelectList FundingSources { get; private set; }
        public SelectList GAPSAssessments { get; private set; }
        public SelectList AbandonedInactive { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            EditStatus = await _repository.GetStatusAsync(id);

            if (EditStatus == null)
            {
                StatusCreateDto newStatus = new StatusCreateDto() { FacilityId = id };

                Guid? newStatusId = await _repository.CreateStatusAsync(newStatus);

                if (newStatusId != null)
                {
                    EditStatus = await _repository.GetStatusAsync(id);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(EditStatus.FacilityId);
            
            if (EditStatus == null || Facility == null)
            {
                return NotFound();
            }
            await PopulateSelectsAsync();
            ActiveTab = "Status";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            try
            {
                await _repository.UpdateStatusAsync(EditStatus.FacilityId, EditStatus);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }
            TempData?.SetDisplayMessage(Context.Success, $"Status successfully Updated.");
            ActiveTab = "Status";
            return RedirectToPage("../Facilities/Details", null, new { id = EditStatus.FacilityId, tab = ActiveTab }, fragment: "TabPages");
        }

        private async Task PopulateSelectsAsync()
        {
            SourceStatuses = await _listHelper.SourceStatusesSelectListAsync();
            SoilStatuses = await _listHelper.SoilStatusesSelectListAsync();
            GroundwaterStatuses = await _listHelper.GroundwaterStatusesSelectListAsync();
            OverallStatuses = await _listHelper.OverallStatusesSelectListAsync();
            FundingSources = await _listHelper.FundingSourceSelectListAsync();
            GAPSAssessments = await _listHelper.GAPSAssessmentSelectListAsync();
            AbandonedInactive = await _listHelper.AbandonedInactiveSelectListAsync();
        }
    }
}
