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

namespace FMS.Pages.Score
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IScoreRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public ScoreEditDto ScoreEditDto { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList ComplianceOfficers { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            ScoreEditDto = await _repository.GetScoreByIdAsync(id);
            if (ScoreEditDto == null)
            {
                return NotFound();
            }

            Facility = await _facilityRepository.GetFacilityAsync(ScoreEditDto.FacilityId);
            if (Facility == null)
            {
                return NotFound();
            }

            await PopulateSelectsAsync();

            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
        }
    }
}
