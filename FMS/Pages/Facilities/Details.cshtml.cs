using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Helpers;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUglify.Helpers;

namespace FMS.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public RetentionRecordCreateDto RecordCreate { get; set; }

        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public Guid FacilityId { get; set; }

        private readonly IFacilityRepository _repository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public DetailsModel(IFacilityRepository repository,
            IEventRepository eventRepository,
            ILocationRepository locationRepository,
            IConfiguration configuration,
            IUserService userService)
        {
            _repository = repository;
            _eventRepository = eventRepository;
            _locationRepository = locationRepository;
            _configuration = configuration;
            _userService = userService;
        }

        public UserView CurrentUser { get; private set; }

        public string GoogleMapsApiKey => _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;

        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        public FacilityDetailDto FacilityDetail { get; set; }

        public IEnumerable<EventSummaryDto> EventSummaryList { get; set; }

        public DisplayMessage Message { get; private set; }

        [TempData]
        public Guid HighlightRecord { get; set; }

        public string RNHSIFolderLink { get; set; } = string.Empty;

        public string HSIFolderLink { get; set; } = string.Empty;

        public string NotificationFolderLink { get; set; } = string.Empty;

        public string PendingNotificationFolderLink { get; set; } = string.Empty;

        public string ComplaintsFolderLink { get; set; } = string.Empty;

        [BindProperty]
        public string MapLink { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        [TempData]
        public string Lat { get; set; }

        [TempData]
        public string Lon { get; set; }

        [BindProperty(SupportsGet = true)]
        [HiddenInput]
        public EventSort SortBy { get; set; }

        public bool IsVrpTracked { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, Guid? hr, string tab, EventSort sortBy = EventSort.StartDateDesc)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacilityDetail = await _repository.GetFacilityAsync(id.Value);

            if (FacilityDetail == null)
            {
                return NotFound();
            }

            Spec = new SiteSummaryQuerySpec
            {
                FacilityNumber = FacilityDetail.FacilityNumber,
                ShowHeader = true
            };

            if (hr.HasValue)
            {
                HighlightRecord = hr.Value;
            }

            if (FacilityDetail.FacilityType.Name == "HSI" || (FacilityDetail.FacilityType.Name == "NPL" && FacilityDetail.FacilityStatus.Name == "EPA Referred"))
            {
                HSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.FacilityNumber);
            }

            if (FacilityDetail.FacilityType.Name == "RN")
            {
                if (FacilityDetail.DeterminationLetterDate.HasValue && string.IsNullOrEmpty(FacilityDetail.HSInumber))
                {
                    NotificationFolderLink = UrlHelper.GetNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                else if (string.IsNullOrEmpty(FacilityDetail.HSInumber) && FacilityDetail.FacilityStatus.Name != "COMPLAINT")
                {
                    PendingNotificationFolderLink = UrlHelper.GetPendingNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                if (FacilityDetail.FacilityStatus.Name == "COMPLAINT")
                {
                    ComplaintsFolderLink = UrlHelper.GetComplaintsFolderLink(FacilityDetail.FacilityNumber);
                }
                else
                {
                    RNHSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.HSInumber);
                }
            }

            CurrentUser = await _userService.GetCurrentUserAsync()
                ?? throw new InvalidOperationException("Current user not found", new Exception("User retrieval failed"));

            ActiveTab = tab ?? "HSIProperties";
            SortBy = sortBy;
            MapLink = GetMapLink();
            FacilityDetail.Events = EventSortHelper.SortEvents(FacilityDetail.Events, SortBy);
            FacilityId = FacilityDetail.Id;
            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnGetEventToggleAsync(Guid eventId, bool eventActive)
        {
            FacilityDetail = await _repository.GetFacilityAsync(FacilityId);

            await _eventRepository.UpdateEventStatusAsync(eventId, eventActive);

            Spec = new SiteSummaryQuerySpec
            {
                FacilityNumber = FacilityDetail.FacilityNumber,
                ShowHeader = true
            };

            if (FacilityDetail.FacilityType.Name == "HSI" || (FacilityDetail.FacilityType.Name == "NPL" && FacilityDetail.FacilityStatus.Name == "EPA Referred"))
            {
                HSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.FacilityNumber);
            }

            if (FacilityDetail.FacilityType.Name == "RN")
            {
                if (FacilityDetail.DeterminationLetterDate.HasValue && string.IsNullOrEmpty(FacilityDetail.HSInumber))
                {
                    NotificationFolderLink = UrlHelper.GetNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                else if (string.IsNullOrEmpty(FacilityDetail.HSInumber) && FacilityDetail.FacilityStatus.Name != "COMPLAINT")
                {
                    PendingNotificationFolderLink = UrlHelper.GetPendingNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                if (FacilityDetail.FacilityStatus.Name == "COMPLAINT")
                {
                    ComplaintsFolderLink = UrlHelper.GetComplaintsFolderLink(FacilityDetail.FacilityNumber);
                }
                else
                {
                    RNHSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.HSInumber);
                }
            }

            CurrentUser = await _userService.GetCurrentUserAsync()
                ?? throw new InvalidOperationException("Current user not found", new Exception("User retrieval failed"));

            ActiveTab = "Events";
            MapLink = GetMapLink();
            FacilityDetail.Events = EventSortHelper.SortEvents(FacilityDetail.Events, SortBy);
            FacilityId = FacilityDetail.Id;
            
            TempData?.SetDisplayMessage(Context.Success,
                eventActive
                    ? $"Event successfully restored to list."
                    : $"Event successfully removed from list.");

            return RedirectToPage(pageName: "Details", routeValues: new { id = FacilityDetail.Id, hr = (Guid?)null, tab = "Events", sortBy = SortBy });
        }

        public async Task<IActionResult> OnPostRetentionRecordAsync()
        {
            if (!ModelState.IsValid)
            {
                FacilityDetail = await _repository.GetFacilityAsync(FacilityId);

                if (FacilityDetail == null)
                {
                    return NotFound();
                }

                if (FacilityDetail.FacilityType.Name == "HSI")
                {
                    HSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.FacilityNumber);
                }

                if (FacilityDetail.FacilityType.Name == "RN")
                {
                    if (!FacilityDetail.HSInumber.IsNullOrWhiteSpace())
                    {
                        NotificationFolderLink = UrlHelper.GetNotificationFolderLink(FacilityDetail.FacilityNumber);
                    }
                    RNHSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.HSInumber);
                }

                FacilityId = FacilityDetail.Id;

                return Page();
            }

            RecordCreate.TrimAll();
            MapLink = GetMapLink();
            HighlightRecord = await _repository.CreateRetentionRecordAsync(FacilityId, RecordCreate);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportButtonAsync(EventSort sortBy)
        {
            var facility = await _repository.GetFacilityAsync(FacilityId);
            var fileName = $"FMS_{facility.Name}_Event_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            // "EventDetailList" Detailed Event List to go to a report
            IList<EventSummaryDto> eventReportList = await _eventRepository.GetEventsByFacilityIdAsync(FacilityId);
            eventReportList = EventSortHelper.SortEvents(eventReportList, sortBy);
            var eventDetailList = from p in eventReportList select new EventSummaryDtoScalar(p, facility.Name, facility.FacilityNumber);
            return File(eventDetailList.ExportExcelAsByteArray(ExportHelper.ReportType.Event), "application/vnd.ms-excel", fileName);
        }

        public string GetMapLink()
        {
            if (FacilityDetail != null && FacilityDetail.Latitude != 0 && FacilityDetail.Longitude != 0)
            {
                Lat = FacilityDetail.Latitude.ToString();
                Lon = FacilityDetail.Longitude.ToString();
                return UrlHelper.GetMapLink(FacilityDetail.Latitude, FacilityDetail.Longitude);
            }
            return string.Empty;
        }

        public async Task<IActionResult> OnGetImageAsync(Guid id)
        {
            FacilityDetail = await _repository.GetFacilityAsync(id);
            var imageFile = await GetMapImage(FacilityDetail);

            if (!string.IsNullOrWhiteSpace(imageFile))
            {
               return PhysicalFile(imageFile, "image/png");
            }
            return null;
        }

        public async Task<string> GetMapImage(FacilityDetailDto facility)
        {
            bool updateMapProperties = false;
            if (facility.LocationDetails.MapType == null || facility.LocationDetails.MapZoom == null)
            {
                updateMapProperties = true;
            }

            var MapProps = new MapProperties(facility.LocationDetails.Id, facility.FacilityNumber, facility.Latitude, facility.Longitude, facility.LocationDetails.MapZoom, facility.LocationDetails.MapType, false, updateMapProperties);

            return await new MapHelper(_configuration, _locationRepository).GetMapImage(MapProps);
        }

        public static bool GetVrpTrackingStatus(FacilityDetailDto facility)
        {
            if (facility != null &&
                facility.HsrpFacilityPropertyDetails.VRPDate != null &&
                !facility.HsrpFacilityPropertyDetails.VRPTerminated)
            {
                return true;
            }
            return false;
        }
    }
}