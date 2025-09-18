using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using FMS.Platform.Extensions;
using FMS.Helpers;
using System.Net;
using NUglify.Helpers;
using Microsoft.IdentityModel.Tokens;

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
        public DetailsModel(IFacilityRepository repository) => _repository = repository;

        public FacilityDetailDto FacilityDetail { get; set; }

        public DisplayMessage Message { get; private set; }

        [TempData]
        public Guid HighlightRecord { get; set; }

        public string RNHSIFolderLink { get; set; } = string.Empty;

        public string HSIFolderLink { get; set; } = string.Empty;

        public string NotificationFolderLink { get; set; } = string.Empty;

        public string PendingNotificationFolderLink { get; set; } = string.Empty;

        public string MapLink
        {
            get
            {
                if (FacilityDetail != null && FacilityDetail.Latitude != 0 && FacilityDetail.Longitude != 0)
                {
                    return UrlHelper.GetMapLink(FacilityDetail.Latitude, FacilityDetail.Longitude);
                }
                return string.Empty;
            }
        }

        [TempData]
        public string ActiveTab { get; set; }

        [TempData]
        public decimal? Latitude { get; set; }

        [TempData]
        public decimal? Longitude { get; set; }

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

            Latitude = FacilityDetail.Latitude != 0 ? FacilityDetail.Latitude : null;
            Longitude = FacilityDetail.Longitude != 0 ? FacilityDetail.Longitude : null;

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

            HighlightRecord = await _repository.CreateRetentionRecordAsync(FacilityId, RecordCreate);

            return RedirectToPage();
        }
       
    }
}