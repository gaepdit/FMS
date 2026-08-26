using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.FacilityType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditAETModel : PageModel
    {
        private readonly IAllowedEventTypeRepository _repository;
        private readonly IFacilityTypeRepository _facilityTypeRepository;
        private readonly IAllowedEventTypeHelper _allowedEventTypeHelper;
        public EditAETModel(
            IAllowedEventTypeRepository repository,
            IFacilityTypeRepository facilityTypeRepository,
            IAllowedEventTypeHelper allowedEventTypeHelper)
        {
            _repository = repository;
            _facilityTypeRepository = facilityTypeRepository;
            _allowedEventTypeHelper = allowedEventTypeHelper;
        }
        public IList<AllowedEventTypeSpec> AllowedEventTypeList { get; set; }

        public DisplayMessage DisplayMessage { get; private set; }

        public string FacilityTypeName { get; private set; }

        [BindProperty]
        public AllowedEventTypeSpec AllowedEventTypeSpec { get; set; }

        [FromRoute]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FacilityTypeName = await _facilityTypeRepository.GetFacilityTypeNameAsync(Id);
            AllowedEventTypeSpec = new AllowedEventTypeSpec();

            if (FacilityTypeName == null)
            {
                return NotFound();
            }

            AllowedEventTypeList = await _allowedEventTypeHelper.GetAllowedEventTypeListByFacilityTypeIdAsync(Id);

            DisplayMessage = TempData?.GetDisplayMessage();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid eventTypeId)
        {
            FacilityTypeName = await _facilityTypeRepository.GetFacilityTypeNameAsync(Id);

            AllowedEventTypeList = await _allowedEventTypeHelper.GetAllowedEventTypeListByFacilityTypeIdAsync(Id);

            AllowedEventTypeSpec = AllowedEventTypeList
                .Any(e => e.EventTypeId == eventTypeId)
                ? AllowedEventTypeList.First(e => e.EventTypeId == eventTypeId)
                : new AllowedEventTypeSpec()
                {
                    FacilityTypeId = Id,
                    EventTypeId = eventTypeId,
                    Active = true
                };

            if (!await _repository.AllowedEventTypeExistsAsync(AllowedEventTypeSpec.FacilityTypeId, AllowedEventTypeSpec.EventTypeId))
            {
                await _repository.CreateAllowedEventTypeAsync(AllowedEventTypeSpec);
            }
            else
            {
                try
                {
                    await _repository.DeleteAllowedEventTypeAsync(AllowedEventTypeSpec.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.AllowedEventTypeExistsAsync(AllowedEventTypeSpec.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            AllowedEventTypeList = await _repository.GetAllowedEventTypeListAsync(Id);

            TempData?.SetDisplayMessage(Context.Success,
                AllowedEventTypeSpec.Active
                    ? $"{MaintenanceOptions.AllowedEventType} \"{AllowedEventTypeSpec.EventTypeName}\" successfully Deleted."
                    : $"{MaintenanceOptions.AllowedEventType} \"{AllowedEventTypeSpec.EventTypeName}\" successfully Added.");

            return RedirectToPage("EditAET");   //, new { id = Id }
        }
    }
}
