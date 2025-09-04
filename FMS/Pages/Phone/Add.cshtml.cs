using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Pages.Maintenance;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Phone
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
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
        
        public Guid ContactId { get; set; }

        public ContactEditDto Contact { get; set; }

        public SelectList PhoneTypes => new(Data.PhoneTypes);

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            ContactId = id.Value;
            NewPhone = new PhoneCreateDto
            {
                ContactId = id.Value,
                Active = true
            };
            
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
                await _repository.CreatePhoneAsync(NewPhone);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                
                return Page();
            }

            Contact = await _contactRepository.GetContactByIdAsync(NewPhone.ContactId);

            TempData?.SetDisplayMessage(Context.Success, $"Phone Number for \"{Contact.GivenName + " " + Contact.FamilyName}\" successfully Added.");

            return RedirectToPage("../Facilities/Details", new { id = Contact.FacilityId, tab = "Contacts" });
        }
    }
}
