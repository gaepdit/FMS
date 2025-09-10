using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
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
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
    public class EditModel : PageModel
    {
        private readonly IPhoneRepository _repository;
        private readonly IContactRepository _contactRepository;
        public EditModel(
            IPhoneRepository repository,
            IContactRepository contactRepository)
        {
            _repository = repository;
            _contactRepository = contactRepository;
        } 

        [BindProperty]
        public PhoneEditDto EditPhone { get; set; }

        [BindProperty]
        public ContactEditDto Contact { get; set; }

        public SelectList PhoneTypes => new(Data.PhoneTypes);

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditPhone = await _repository.GetPhoneByIdAsync(id);

            if (EditPhone == null)
            {
                return NotFound();
            }

            Contact = await _contactRepository.GetContactByIdAsync(EditPhone.ContactId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Contact = await _contactRepository.GetContactByIdAsync(EditPhone.ContactId);
                return Page();
            }
            try
            {
                await _repository.UpdatePhoneAsync(EditPhone);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
                Contact = await _contactRepository.GetContactByIdAsync(EditPhone.ContactId);
                return Page();
            }
            Contact = await _contactRepository.GetContactByIdAsync(EditPhone.ContactId);

            TempData?.SetDisplayMessage(Context.Success, $"Phone Number for \"{Contact.GivenName + " " + Contact.FamilyName}\" successfully updated.");

            return RedirectToPage("../Facilities/Details", new { id = Contact.FacilityId, tab = "Contacts" });
        }
    }
}
