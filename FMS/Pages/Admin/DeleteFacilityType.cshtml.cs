using System;
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
    public class DeleteFacilityTypeModel : PageModel
    {
        [BindProperty]
        public bool Delete { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public bool ShowChange { get; set; }

        private readonly IFacilityTypeRepository _facilityTypeRepository;
        public DeleteFacilityTypeModel(IFacilityTypeRepository facilityTypeRepository) => _facilityTypeRepository = facilityTypeRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityType = await _facilityTypeRepository.GetFacilityTypeAsync(id.Value);

            if (facilityType == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Delete = !facilityType.Active;
            Name = $"{facilityType.Name} ({facilityType.Description})";
            ShowChange = false;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Name = (await _facilityTypeRepository.GetFacilityTypeAsync(Id)).Name;
                return Page();
            }

            try
            {
                await _facilityTypeRepository.UpdateFacilityTypeStatusAsync(Id, !Delete);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _facilityTypeRepository.FacilityTypeExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Name = (await _facilityTypeRepository.GetFacilityTypeAsync(Id)).Name;
            ShowChange = true;

            return Page();
        }
    }
}
