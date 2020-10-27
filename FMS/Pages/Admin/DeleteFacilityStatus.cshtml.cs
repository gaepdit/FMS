using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class DeleteFacilityStatusModel : PageModel
    {
        [BindProperty]
        public bool Delete { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public bool ShowChange { get; set; }

        private readonly IFacilityStatusRepository _facilityStatusRepository;
        public DeleteFacilityStatusModel(IFacilityStatusRepository facilityStatusRepository) => _facilityStatusRepository = facilityStatusRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityStatus = await _facilityStatusRepository.GetFacilityStatusAsync(id.Value);

            if (facilityStatus == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Delete = !facilityStatus.Active;
            Status = facilityStatus.Status;
            ShowChange = false;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Status = (await _facilityStatusRepository.GetFacilityStatusAsync(Id)).Status;
                return Page();
            }

            try
            {
                await _facilityStatusRepository.UpdateFacilityStatusStatusAsync(Id, !Delete);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _facilityStatusRepository.FacilityStatusExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Status = (await _facilityStatusRepository.GetFacilityStatusAsync(Id)).Status;
            ShowChange = true;

            return Page();
        }
    }
}
