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

namespace FMS.Pages.Contact
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class AddModel : PageModel
    {
        private readonly IContactRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            IContactRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public ContactCreateDto NewContact { get; set; }

        public SelectList ContactTypes { get; private set; }

        public static SelectList States => new(Data.States);

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            NewContact = new ContactCreateDto
            {
                FacilityId = id.Value,
                Active = true
            };
            await PopulateSelectsAsync();
            ActiveTab = "Contacts";
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
                await _repository.CreateContactAsync(NewContact);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the contact. Please try again. Error Detail: \"{ex}\"");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Contact: \"{NewContact.GivenName + " " + NewContact.FamilyName}\" successfully added.");
            ActiveTab = "Contacts";
            return RedirectToPage("../Facilities/Details", new { id = NewContact.FacilityId, tab = "Contacts" });
        }

        private async Task PopulateSelectsAsync()
        {
            ContactTypes = await _listHelper.ContactTypesSelectListAsync();
        }
    }
}
