using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Facilities
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditRetentionRecordModel : PageModel
    {
        [BindProperty]
        public RetentionRecordEditDto RecordEdit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [TempData]
        public Guid HighlightRecord { get; set; }

        public FacilityBasicDto Facility { get; set; }

        private readonly IFacilityRepository _repository;
        public EditRetentionRecordModel(IFacilityRepository repository) => _repository = repository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            RecordEdit = new RetentionRecordEditDto(await _repository.GetRetentionRecordAsync(id.Value));

            if (RecordEdit == null)
            {
                return NotFound();
            }

            Facility = await _repository.GetFacilityForRetentionRecord(Id);

            if (!Facility.Active)
            {
                return RedirectToPage("./Details", new {id = Facility.Id});
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RecordEdit.TrimAll();

            try
            {
                await _repository.UpdateRetentionRecordAsync(Id, RecordEdit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.RetentionRecordExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            var facility = await _repository.GetFacilityForRetentionRecord(Id);
            HighlightRecord = Id;
            return RedirectToPage("./Details", new {id = facility.Id});
        }
    }
}