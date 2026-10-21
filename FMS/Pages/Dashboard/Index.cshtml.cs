using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Dashboard
{
    public class IndexModel(
        IUserService userService,
        IDashboardRepository _repository,
        IUserProgramRepository userProgramRepository,
        IOrganizationalUnitRepository userUnitRepository,
        IUserPositionRepository userPositionRepository) : PageModel
    {
        public UserView CurrentUser { get; private set; }
        public string UserName { get; set; }
        public Guid UserCOId { get; set; } = Guid.Empty;
        public IList<string> Roles { get; private set; }
        public IList<DashboardUserFacilitiesDto> UserFacilities { get; set; } = [];
        public IList<DashboardUnitFacilitiesDto> UnitFacilities { get; set; } = [];
        public IList<DashboardProgramFacilitiesDto> ProgramFacilities { get; set; } = [];
        public IList<DashboardProgramFacilitiesWithDeletedItemsDto> ProgramFacilitiesWithDeletedItems { get; set; } = [];
        public IList<DashboardUserEventsDto> UserEvents { get; set; } = [];
        public IList<DashboardUnitEventsDto> UnitEvents { get; set; } = [];
        public IList<DashboardProgramEventsDto> ProgramEvents { get; set; } = [];
        public IList<string> ProgramUnits { get; set; } = [];
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

            if (CurrentUser.DisplayName == "Test User")
            {
                // Add Test User Program, Unit and Position
                CurrentUser.UserProgram = await userProgramRepository.GetUserProgramByNameAsync("Response and Remediation");
                CurrentUser.UserUnit = await userUnitRepository.GetUnitByNameAsync("Response Development 1");
                CurrentUser.UserPosition = await userPositionRepository.GetPositionByNameAsync("PM1");
                await userService.UpdateUserDesignationsAsync(CurrentUser.Id, CurrentUser.UserProgram.Id, CurrentUser.UserUnit.Id, CurrentUser.UserPosition.Id);
            }

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

                ProgramFacilitiesWithDeletedItems = await _repository.GetProgramFacilitiesWithDeletedItems(CurrentUser.Id);

                ProgramUnits = ProgramFacilities.Select(e => e.OrganizationalUnit?.Name).Distinct().OrderBy(name => name).ToList();
            }

            //Get Events for the current user based on their role and position
            if(CurrentUser.UserPosition.Name != "PM2")
            {
                UserEvents = await _repository.GetEventsByUserId(UserCOId);
            }

            if (CurrentUser.UserPosition.Name == "PM1")
            {
                UnitEvents = await _repository.GetUnitEventsByUserId(CurrentUser.Id);

                UserCOs = UnitEvents.Select(e => e.ComplianceOfficer?.Name).Where(name => name != null).Distinct().OrderBy(name => name).ToList();
            }

            if (CurrentUser.UserPosition.Name == "PM2")
            {
                ProgramEvents = await _repository.GetProgramEventsByUserId(CurrentUser.Id, includeInactive: true);

                UserUnits = ProgramEvents.Select(e => e.Unit?.Name).Where(name => name != null).Distinct().OrderBy(name => name).ToList();

                UserCOs = ProgramEvents.Select(e => e.ComplianceOfficer?.Name).Where(name => name != null).Distinct().OrderBy(name => name).ToList();
            }

            return Page();
        }
    }
}
