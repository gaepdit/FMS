using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Event
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class DeleteModel : PageModel
    {
        private readonly IEventRepository _repository;

        public DeleteModel(IEventRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }
        public EventSummaryDto EventDetail { get; set; }

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

            EventDetail = await _repository.GetEventSummaryByIdAsync(id.Value);
            if (EventDetail == null)
            {
                return NotFound();
            }

            FacilityId = EventDetail.FacilityId;

            Id = id.Value;

            ActiveTab = "Events";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _repository.DeleteEventByIdAsync(Id);

            TempData?.SetDisplayMessage(Context.Success, "Event deleted.");

            ActiveTab = "Events";

            return RedirectToPage("../Facilities/Details", null, new { id = FacilityId, tab = ActiveTab }, fragment: "TabPages");
        }
    }
}
