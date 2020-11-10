using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditFacilityStatusModel : PageModel
    {
        [BindProperty]
        public FacilityStatusEditDto FacilityStatus { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        public EditFacilityStatusModel(IFacilityStatusRepository facilityStatusRepository) =>
            _facilityStatusRepository = facilityStatusRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            FacilityStatus = await _facilityStatusRepository.GetFacilityStatusAsync(id.Value);

            if (FacilityStatus == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FacilityStatus.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _facilityStatusRepository.FacilityStatusStatusExistsAsync(FacilityStatus.Status, Id))
            {
                ModelState.AddModelError("FacilityStatus.Status", "Status entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _facilityStatusRepository.UpdateFacilityStatusAsync(Id, FacilityStatus);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _facilityStatusRepository.FacilityStatusExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"Facility Status {FacilityStatus.Status} successfully updated.");

            return RedirectToPage("./Index", "select", new {MaintenanceSelection = MaintenanceOptions.FacilityStatus});
        }
    }
}