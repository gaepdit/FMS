using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.EventType
{
    public class EditAATReqModel : PageModel
    {
        private readonly IAllowedActionTakenRepository _repository;

        public EditAATReqModel(IAllowedActionTakenRepository repository)
        {
            _repository = repository;
        }

        public DisplayMessage DisplayMessage { get; private set; }

        public string ActionTakenName { get; private set; }

        public string EventTypeName { get; private set; }

        [BindProperty]
        public AllowedActionTakenSpec AllowedActionTakenSpec { get; set; }

        [FromRoute]
        public Guid Id { get; set; }

        public async Task OnGet()
        {
            AllowedActionTakenSpec = await _repository.GetAllowedActionTakenByAATIdAsync(Id);
            ActionTakenName = AllowedActionTakenSpec.ActionTakenName;
            EventTypeName = AllowedActionTakenSpec.EventTypeName;
        }
    }
}
