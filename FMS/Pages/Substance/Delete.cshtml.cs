using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Substance
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class DeleteModel : PageModel
    {
        private readonly ISubstanceRepository _repository;

        public DeleteModel(ISubstanceRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }

        public SubstanceSummaryDto SubstanceDetail { get; set; }

        [BindProperty]
        public Guid FacilityId { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SubstanceDetail = await _repository.GetSubstanceSummaryByIdAsync(id.Value);
            if (SubstanceDetail == null)
            {
                return NotFound();
            }
            FacilityId = SubstanceDetail.FacilityId;

            Id = id.Value;
            ActiveTab = "Substances";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repository.DeleteSubstanceAsync(Id);
            TempData?.SetDisplayMessage(Context.Success, "Substance deleted.");
            ActiveTab = "Substances";
            return RedirectToPage("/Facilities/Details", new { id = FacilityId, hr = Guid.Empty });
        }
    }
}
