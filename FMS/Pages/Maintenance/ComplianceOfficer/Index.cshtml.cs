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

namespace FMS.Pages.Maintenance.ComplianceOfficer
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IComplianceOfficerRepository _repository;
        public IndexModel(IComplianceOfficerRepository repository) => _repository = repository;

        public IReadOnlyList<ComplianceOfficerSummaryDto> ComplianceOfficers { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ComplianceOfficers = await _repository.GetComplianceOfficerListAsync();
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

            var complianceOfficer = await _repository.GetComplianceOfficerAsync(itemId.Value);

            if (complianceOfficer == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateComplianceOfficerStatusAsync(itemId.Value, !complianceOfficer.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ComplianceOfficerIdExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            ComplianceOfficers = await _repository.GetComplianceOfficerListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                complianceOfficer.Active
                    ? $"{MaintenanceOptions.ComplianceOfficer} \"{complianceOfficer.DisplayName}\" successfully removed from list."
                    : $"{MaintenanceOptions.ComplianceOfficer} \"{complianceOfficer.DisplayName}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}