using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.ObjectModel;
using System.Linq;

namespace FMS.Pages.Dashboard
{
    public class IndexModel(
        IAuthorizationService authorization,
        IUserService userService,
        IDashboardRepository _repository) : PageModel
    {
        public UserView CurrentUser { get; private set; }
        public string UserName { get; set; }
        public Guid UserCOId { get; set; } = Guid.Empty;
        public IList<string> Roles { get; private set; }
        public IList<DashboardUserFacilitiesDto> UserFacilities { get; set; } = new List<DashboardUserFacilitiesDto>();
        public IList<DashboardUnitFacilitiesDto> UnitFacilities { get; set; } = new List<DashboardUnitFacilitiesDto>();
        public IList<DashboardProgramFacilitiesDto> ProgramFacilities { get; set; } = new List<DashboardProgramFacilitiesDto>();
        public IList<DashboardUserEventsDto> UserEvents { get; set; } = new List<DashboardUserEventsDto>();
        public IList<DashboardUnitEventsDto> UnitEvents { get; set; } = new List<DashboardUnitEventsDto>();
        public IList<DashboardProgramEventsDto> ProgramEvents { get; set; } = new List<DashboardProgramEventsDto>();
        public IList<OrganizationalUnit> UserUnits { get; set; } 

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

            UserFacilities = await _repository.GetUserHSIFacilitiesById(UserCOId);

            if (CurrentUser.UserPosition.Name == "PM1")
            {
                UnitFacilities = await _repository.GetUnitHSIFacilitiesById(CurrentUser.Id);
            }

            if (CurrentUser.UserPosition.Name == "PM2")
            {
                ProgramFacilities = await _repository.GetProgramHSIFacilitiesById(CurrentUser.UserProgram.Id);
            }

            //Get Events for the current user based on their role and position

            UserEvents = await _repository.GetEventsByUserId(UserCOId);

            if (CurrentUser.UserPosition.Name == "PM1")
            {
                UnitEvents = await _repository.GetUnitEventsByUserId(CurrentUser.Id);
            }

            if (CurrentUser.UserPosition.Name == "PM2")
            {
                ProgramEvents = await _repository.GetProgramEventsByUserId(CurrentUser.Id);

                UserUnits = [.. ProgramEvents
                    .Where(programEvent => programEvent.Unit != null)
                    .Select(programEvent => programEvent.Unit)
                    .Distinct()
                    .ToList()];
            }
                    

            return Page();
        }
    }
}
