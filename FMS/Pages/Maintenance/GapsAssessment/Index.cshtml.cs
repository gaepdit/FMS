using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.GapsAssessment
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IGapsAssessmentRepository _repository;
        public IndexModel(IGapsAssessmentRepository repository) => _repository = repository;

        public IReadOnlyList<GapsAssessmentSummaryDto> GapsAssessment { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GapsAssessment = await _repository.GetGapsAssessmentListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var gapsAssessment = await _repository.GetGapsAssessmentByIdAsync(itemId.Value);

            if (gapsAssessment == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateGapsAssessmentStatusAsync(itemId.Value, !gapsAssessment.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.GapsAssessmentExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            GapsAssessment = await _repository.GetGapsAssessmentListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                gapsAssessment.Active
                    ? $"{MaintenanceOptions.GapsAssessment} \"{gapsAssessment.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.GapsAssessment} \"{gapsAssessment.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
