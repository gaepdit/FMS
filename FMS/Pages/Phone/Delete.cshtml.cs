using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Phone
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class DeleteModel : PageModel
    {
        private readonly IPhoneRepository _repository;
        private readonly IContactRepository _contactRepository;
        public DeleteModel(IPhoneRepository repository, IContactRepository contactRepository)
        {
            _repository = repository;
            _contactRepository = contactRepository;
        }

        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }
        public PhoneSummaryDto PhoneDetail { get; set; }

        public ContactSummaryDto ContactDetail { get; set; }

        [BindProperty]
        public Guid FacilityId { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhoneDetail = await _repository.GetPhoneSummaryByIdAsync(id.Value);
            if (PhoneDetail == null)
            {
                return NotFound();
            }
            ContactDetail = await _contactRepository.GetContactSummaryByIdAsync(PhoneDetail.ContactId);
            if(ContactDetail == null)
            {
                return NotFound();
            }

            FacilityId = ContactDetail.FacilityId;

            Id = id.Value;

            ActiveTab = "Phones";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _repository.DeletePhoneByIdAsync(Id))
            {
                TempData?.SetDisplayMessage(Context.Success, "Phone deleted.");
            }
            else
            {
                TempData?.SetDisplayMessage(Context.Danger, "Phone could not be deleted.");
            }

            ActiveTab = "Contacts";

            return RedirectToPage("../Facilities/Details", null, new { id = FacilityId, tab = ActiveTab });
        }
    }
}
