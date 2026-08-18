using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Dashboard
{
    public class IndexModel(
        IUserService userService,
        IDashboardRepository _repository) : PageModel
    {
        public UserView CurrentUser { get; private set; }
        public string UserName { get; set; }
        public Guid UserCOId { get; set; } = Guid.Empty;
        public IList<string> Roles { get; private set; }
        public IList<DashboardUserFacilitiesDto> UserFacilities { get; set; } = [];
        public IList<DashboardUnitFacilitiesDto> UnitFacilities { get; set; } = [];
        public IList<DashboardProgramFacilitiesDto> ProgramFacilities { get; set; } = [];
        public IList<DashboardUserEventsDto> UserEvents { get; set; } = [];
        public IList<DashboardUnitEventsDto> UnitEvents { get; set; } = [];
        public IList<DashboardProgramEventsDto> ProgramEvents { get; set; } = [];
        public IList<string> UserUnits { get; set; } = [];
        public IList<string> UserCOs { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity is not { IsAuthenticated: true })
                return Challenge();

            CurrentUser = await userService.GetCurrentUserAsync()
                ?? throw new InvalidOperationException("Current user not found", new Exception("User retrieval failed"));

            Roles = await userService.GetCurrentUserRolesAsync();

            UserCOId = await _repository.GetCOIdFromUser(CurrentUser);

            if (UserCOId == Guid.Empty || CurrentUser.UserPosition == null)
            {
                return RedirectToPage("/Facilities/Index");
            }

            UserName = CurrentUser.DisplayName;

            // Get Facilities for the current user based on their role and position
            if(CurrentUser.UserPosition.Name != "PM2")
            {
                UserFacilities = await _repository.GetUserHSIFacilitiesById(UserCOId);
            }

            if (CurrentUser.UserPosition.Name == "PM1")
            {
                UnitFacilities = await _repository.GetUnitHSIFacilitiesById(CurrentUser.Id);
            }

            if (CurrentUser.UserPosition.Name == "PM2")
            {
                ProgramFacilities = await _repository.GetProgramHSIFacilitiesById(CurrentUser.UserProgram.Id);


            }

            //Get Events for the current user based on their role and position
            if(CurrentUser.UserPosition.Name != "PM2")
            {
                UserEvents = await _repository.GetEventsByUserId(UserCOId);
            }

            if (CurrentUser.UserPosition.Name == "PM1")
            {
                UnitEvents = await _repository.GetUnitEventsByUserId(CurrentUser.Id);

                foreach (var unitEvent in UnitEvents.Where(unitEvent => !UserCOs.Contains(unitEvent.ComplianceOfficer?.Name) && unitEvent.ComplianceOfficer?.Name != null))
                {
                    UserCOs.Add(unitEvent.ComplianceOfficer?.Name);
                }
            }

            if (CurrentUser.UserPosition.Name == "PM2")
            {
                ProgramEvents = await _repository.GetProgramEventsByUserId(CurrentUser.Id);

                foreach (var programEvent in ProgramEvents.Where(programEvent => !UserUnits.Contains(programEvent.Unit?.Name) && programEvent.Unit?.Name != null))
                {
                    UserUnits.Add(programEvent.Unit?.Name);
                }

                foreach (var programEvent in ProgramEvents.Where(programEvent => !UserCOs.Contains(programEvent.ComplianceOfficer?.Name) && programEvent.ComplianceOfficer?.Name != null))
                {
                    UserCOs.Add(programEvent.ComplianceOfficer?.Name);
                }
            }

            return Page();
        }
    }
}
