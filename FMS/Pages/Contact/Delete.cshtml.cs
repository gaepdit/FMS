using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Contact
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class DeleteModel : PageModel
    {
        private readonly IContactRepository _repository;
        private readonly IPhoneRepository _phoneRepository;
        public DeleteModel(IContactRepository repository, IPhoneRepository phoneRepository)
        {
            _repository = repository;
            _phoneRepository = phoneRepository;
        }

        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }
        public ContactSummaryDto ContactDetail { get; set; }

        [BindProperty]
        public Guid FacilityId { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public string ScreenMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                ContactDetail = null;
                ScreenMessage = "Contact ID Is Not Found.";
                return Page();
            }

            ContactDetail = await _repository.GetContactSummaryByIdAsync(id.Value);
            if (ContactDetail == null)
            {
                ContactDetail = null;
                ScreenMessage = "Contact Information Is Not Found.";
                return Page();
            }

            FacilityId = ContactDetail.FacilityId;

            Id = id.Value;

            ActiveTab = "Events";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ContactDetail = await _repository.GetContactSummaryByIdAsync(Id);
            if (ContactDetail == null)
            {
                return NotFound();
            }
            FacilityId = ContactDetail.FacilityId;

            ActiveTab = "Contacts";
            try
            {
                foreach(var item in ContactDetail.Phones)
                {
                    await _phoneRepository.DeletePhoneByIdAsync(item.Id);
                }
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Could Not Delete Phones: {ex.Message}");
                return RedirectToPage("../Facilities/Details", null, new { id = FacilityId, tab = ActiveTab });
            }

            try
            {
                if (await _repository.DeleteContactByIdAsync(Id))
                {
                    TempData?.SetDisplayMessage(Context.Success, "Contact and all associated Phones deleted.");
                }
                else
                {
                    TempData?.SetDisplayMessage(Context.Danger, "Contact could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Error deleting contact: {ex.Message}");
                return RedirectToPage("../Facilities/Details", null, new { id = FacilityId, tab = ActiveTab });
            }

            return RedirectToPage("../Facilities/Details", null, new { id = FacilityId, tab = ActiveTab });
        }
    }
}
