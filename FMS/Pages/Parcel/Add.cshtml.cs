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

namespace FMS.Pages.Parcel
{
    [Authorize(Policy = UserPolicies.FileEditor)]
    public class AddModel : PageModel
    {
        private readonly IParcelRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            IParcelRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public ParcelCreateDto NewParcel { get; set; }

        public SelectList ParcelTypes { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            NewParcel = new ParcelCreateDto
            {
                FacilityId = id.Value,
                Active = true
            };

            await PopulateSelectsAsync();
            ActiveTab = "Location";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewParcel.TrimAll();

            if (await _repository.ParcelNameExistsAsync(NewParcel.ParcelNumber))
            {
                ModelState.AddModelError(string.Empty, $"Parcel with Parcel Number \"{NewParcel.ParcelNumber}\" already exists");
                await PopulateSelectsAsync();
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateParcelAsync(NewParcel);

            TempData?.SetDisplayMessage(Context.Success, $"Parcel Number \"{NewParcel.ParcelNumber}\" successfully created.");
            ActiveTab = "Location";
            return RedirectToPage("../Facilities/Details", new { id = NewParcel.FacilityId, tab = ActiveTab });
        }

        private async Task PopulateSelectsAsync()
        {
            ParcelTypes = await _listHelper.ParcelTypesSelectListAsync();
        }
    }
}
