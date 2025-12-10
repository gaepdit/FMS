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

namespace FMS.Pages.Contact
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class EditModel : PageModel
    {
        private readonly IContactRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IContactRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public ContactEditDto EditContact { get; set; }

        public SelectList ContactTypes { get; private set; }

        public static SelectList States => new(Data.States);

        [BindProperty]
        public Guid Id { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditContact = await _repository.GetContactByIdAsync(id);
            if (EditContact == null)
            {
                return NotFound();
            }
            await PopulateSelectsAsync();
            ActiveTab = "Contacts";
            return Page();
        }

        public
            async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }
            try
            {
                await _repository.UpdateContactAsync(Id, EditContact);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Contact: \"{EditContact.GivenName + " " + EditContact.FamilyName}\" successfully updated.");
            ActiveTab = "Contacts";
            return RedirectToPage("../Facilities/Details", new { id = EditContact.FacilityId, tab = ActiveTab });
        }

        private async Task PopulateSelectsAsync()
        {
            ContactTypes = await _listHelper.ContactTypesSelectListAsync();
        }
    }
}
