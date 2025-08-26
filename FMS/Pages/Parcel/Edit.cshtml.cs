using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Parcel
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
    public class EditModel : PageModel
    {
        private readonly IParcelRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IParcelRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public ParcelEditDto EditParcel { get; set; }

        public SelectList ParcelTypes { get; private set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditParcel = await _repository.GetParcelByIdAsync(id);
            if (EditParcel == null)
            {
                return NotFound();
            }
            await PopulateSelectsAsync();
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
                await _repository.UpdateParcelAsync(EditParcel.Id, EditParcel);
                return RedirectToPage("../Facilities/Details", new { id = EditParcel.FacilityId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while updating the parcel. Please try again. Error Detail: \"{ex}\"");
                await PopulateSelectsAsync();
                return Page();
            }
        }

        private async Task PopulateSelectsAsync()
        {
            ParcelTypes = await _listHelper.ParcelTypesSelectListAsync();
        }
    }
}
