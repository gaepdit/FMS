using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public RetentionRecordCreateDto RecordCreate { get; set; }

        [BindProperty]
        [HiddenInput]
        public Guid FacilityId { get; set; }

        private readonly IFacilityRepository _repository;
        private readonly IEventRepository _eventRepository;
        public DetailsModel(IFacilityRepository repository, IEventRepository eventRepository)
        {
            _repository = repository;
            _eventRepository = eventRepository;
        }

        public FacilityDetailDto FacilityDetail { get; set; }

        public DisplayMessage Message { get; private set; }

        [TempData]
        public Guid HighlightRecord { get; set; }

        public string RNHSIFolderLink { get; set; } = string.Empty;

        public string HSIFolderLink { get; set; } = string.Empty;

        public string NotificationFolderLink { get; set; } = string.Empty;

        public string PendingNotificationFolderLink { get; set; } = string.Empty;

        [BindProperty]
        public string MapLink { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        [TempData]
        public string Lat { get; set; }

        [TempData]
        public string Lon { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, Guid? hr)
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

            if (hr.HasValue)
            {
                HighlightRecord = hr.Value;
            }

            if(FacilityDetail.FacilityType.Name == "HSI")
            {
                HSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.FacilityNumber);
            }
            
            if (FacilityDetail.FacilityType.Name == "RN")
            {
                if (FacilityDetail.DeterminationLetterDate.HasValue && string.IsNullOrEmpty(FacilityDetail.HSInumber))
                {
                    NotificationFolderLink = UrlHelper.GetNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                else if (string.IsNullOrEmpty(FacilityDetail.HSInumber))
                {
                    PendingNotificationFolderLink = UrlHelper.GetPendingNotificationFolderLink(FacilityDetail.FacilityNumber);
                }
                RNHSIFolderLink = UrlHelper.GetHSIFolderLink(FacilityDetail.HSInumber);
            }
            
            if (string.IsNullOrEmpty(ActiveTab))
            {
                ActiveTab = "HSIProperties";
            }

            MapLink = GetMapLink();
            FacilityDetail.Events = EventSortHelper.SortEvents(FacilityDetail.Events);
            FacilityId = FacilityDetail.Id;
            Message = TempData?.GetDisplayMessage();
            return Page();
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
            GetMapLink();
            HighlightRecord = await _repository.CreateRetentionRecordAsync(FacilityId, RecordCreate);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostExportButtonAsync()
        {
            var facility = await _repository.GetFacilityAsync(FacilityId);
            var fileName = $"FMS_{facility.Name}_Event_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            // "EventDetailList" Detailed Event List to go to a report
            IReadOnlyList<EventSummaryDto> eventReportList = (IReadOnlyList<EventSummaryDto>)await _eventRepository.GetEventsByFacilityIdAsync(FacilityId);
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
    }
}