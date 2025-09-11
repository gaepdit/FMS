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

namespace FMS.Pages.Substance
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
    public class EditModel : PageModel
    {
        private readonly ISubstanceRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            ISubstanceRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public SubstanceEditDto EditSubstance { get; set; }

        public SelectList Chemicals { get; private set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditSubstance = await _repository.GetSubstanceByIdAsync(id);

            if (EditSubstance == null)
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
                await _repository.UpdateSubstanceAsync(EditSubstance.Id, EditSubstance);
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Substance was NOT Updated. Error Occured: ");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Substance successfully Updated.");

            return RedirectToPage("../Facilities/Details", new { id = EditSubstance.FacilityId });
        }

        private async Task PopulateSelectsAsync()
        {
            Chemicals = await _listHelper.ChemicalsSelectListAsync();
        }
    }
}
