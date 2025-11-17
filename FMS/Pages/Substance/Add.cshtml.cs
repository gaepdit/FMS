using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Infrastructure.Repositories;
using FMS.Pages.Maintenance;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Substance
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
    public class AddModel : PageModel
    {
        private readonly ISubstanceRepository _repository;
        private readonly IChemicalRepository _chemicalRepository;
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            ISubstanceRepository repository,
            IChemicalRepository chemicalRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _chemicalRepository = chemicalRepository;
            _listHelper = listHelper;
        }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SubstanceCreateDto NewSubstance { get; set; }

        public IEnumerable<SubstanceSummaryDto> Substances { get; set; }

        public SelectList Chemicals { get; private set; }

        public bool ResultsActive { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            Id = id.Value;
            NewSubstance = new SubstanceCreateDto
            {
                FacilityId = id.Value,
                Active = true
            };

            await PopulateSelectsAsync();
            ActiveTab = "Substances";
            ResultsActive = false;
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(SubstanceCreateDto newSubstance)
        {
            NewSubstance = newSubstance;
            Id = NewSubstance.FacilityId;
            NewSubstance.ChemicalId = NewSubstance.Chemical?.Id ?? Guid.Empty;
            NewSubstance.Chemical = await _chemicalRepository.GetChemicalByChemIdAsync(NewSubstance.ChemicalId);

            if (NewSubstance.Chemical == null)
            {
                ModelState.AddModelError(string.Empty, "The selected chemical was not found.");
                ResultsActive = false;
                await PopulateSelectsAsync();
                return Page();
            }

            if (await _repository.SubstanceExistsForChemicalAsync(NewSubstance.ChemicalId, NewSubstance.FacilityId))
            {
                ModelState.AddModelError(string.Empty, "A substance for the selected chemical already exists.");
                ResultsActive = false;
                await PopulateSelectsAsync();
                return Page();
            }

            await PopulateSelectsAsync();
            ActiveTab = "Substances";
            ResultsActive = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            if (!NewSubstance.Soil && NewSubstance.UseForSoilScoring)
            {
                ModelState.AddModelError(string.Empty, "Cannot use for Onsite Scoring if not marked as present in Soil.");
                await PopulateSelectsAsync();
                return Page();
            }

            if (!NewSubstance.Groundwater && NewSubstance.UseForGroundwaterScoring)
            {
                ModelState.AddModelError(string.Empty, "Cannot use for Groundwater Scoring if not marked as present in Groundwater.");
                await PopulateSelectsAsync();
                return Page();
            }

            if (NewSubstance.Soil && NewSubstance.UseForSoilScoring && await _repository.SubstanceUsedForOnsiteScoreForFacilityExistsAsync(NewSubstance.FacilityId))
            {
                ModelState.AddModelError(string.Empty, "A Substance for use in Onsite Scoring already exists for this Facility.");
                ResultsActive = true;
                await PopulateSelectsAsync();
                return Page();
            }

            if (NewSubstance.Groundwater && NewSubstance.UseForGroundwaterScoring && await _repository.SubstanceUsedForGroundwaterScoreForFacilityExistsAsync(NewSubstance.FacilityId))
            {
                ModelState.AddModelError(string.Empty, "A Substance for use in Groundwater Scoring already exists.");
                ResultsActive = true;
                await PopulateSelectsAsync();
                return Page();
            }

            try
            {
                await _repository.CreateSubstanceAsync(NewSubstance);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, "The substance was created successfully.");
            ActiveTab = "Substances";
            return RedirectToPage("../Facilities/Details", new { id = NewSubstance.FacilityId });
        }

        private async Task PopulateSelectsAsync()
        {
            Chemicals = await _listHelper.ChemicalsSelectListAsync();
            Substances = await _repository.GetSubstanceListByFacilityIdAsync(Id);
        }
    }
}
