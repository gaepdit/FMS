using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.OnsiteScore
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IOnsiteScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;

        public EditModel(
            IOnsiteScoreRepository repository,
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        [BindProperty]
        public OnsiteScoreEditDto OnsiteScore { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public Chemical Chemical { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            OnsiteScore = await _repository.GetOnsiteScoreByFacilityIdAsync(id);

            if (OnsiteScore == null)
            {
                OnsiteScoreCreateDto newOnsiteScore = new OnsiteScoreCreateDto() { FacilityId = id };
                Guid? newOnsiteScoreId = await _repository.CreateOnsiteScoreAsync(newOnsiteScore);
                if (newOnsiteScoreId != null)
                {
                    OnsiteScore = await _repository.GetOnsiteScoreByFacilityIdAsync(id);
                }
                else
                {
                    return NotFound();
                }
            }

            if (OnsiteScore.SubstanceId == null)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Substance for Onsite Scoring has not been chosen. Please choose a Substance for Onsite Scoring.");
                ActiveTab = "Substances";
                return RedirectToPage("../Facilities/Details", new { id });
            }

            Chemical = OnsiteScore.Substance?.Chemical;

            OnsiteScore.CASNO = Chemical?.CasNo;
            OnsiteScore.ChemName1D = Chemical?.ChemicalName;
            OnsiteScore.Other1D = Chemical?.CommonName;

            Facility = await _facilityRepository.GetFacilityAsync(OnsiteScore.FacilityId);
            
            if (OnsiteScore == null || Facility == null)
            {
                return NotFound();
            }

            ActiveTab = "Score";
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
                await _repository.UpdateOnsiteScoreAsync(OnsiteScore);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Onsite Score successfully Updated.");
            ActiveTab = "Score";
            return RedirectToPage("../Facilities/Details", new { id = OnsiteScore.FacilityId });
        }
    }
}
