using FMS.Domain.Data;
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

namespace FMS.Pages.Phone
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class AddModel : PageModel
    {
        private readonly IPhoneRepository _repository;
        private readonly IContactRepository _contactRepository;

        public AddModel(
            IPhoneRepository repository,
            IContactRepository contactRepository)
        {
            _repository = repository;
            _contactRepository = contactRepository;
        }

        [BindProperty]
        public PhoneCreateDto NewPhone { get; set; }

        public ContactEditDto Contact { get; set; }

        public SelectList PhoneTypes => new(Data.PhoneTypes);

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            Contact = await _contactRepository.GetContactByIdAsync(id.Value);
            if (Contact == null)
            {
                return NotFound();
            }

            NewPhone = new PhoneCreateDto
            {
                ContactId = id.Value,
                Active = true
            };
            ActiveTab = "Contacts";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                NewPhone = PhoneCreateDto.FormatNumber(NewPhone);
                await _repository.CreatePhoneAsync(NewPhone);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                
                return Page();
            }

            Contact = await _contactRepository.GetContactByIdAsync(NewPhone.ContactId);

            TempData?.SetDisplayMessage(Context.Success, $"Phone Number for \"{Contact.GivenName + " " + Contact.FamilyName}\" successfully Added.");
            ActiveTab = "Contacts";
            return RedirectToPage("../Facilities/Details", null, new { id = Contact.FacilityId, tab = ActiveTab }, fragment: "TabPages");
        }
    }
}
