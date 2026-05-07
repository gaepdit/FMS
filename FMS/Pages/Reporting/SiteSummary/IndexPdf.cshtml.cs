using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class IndexPdfModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly IConfiguration _configuration;

        public IndexPdfModel(IReportingRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        [BindProperty]
        public IReadOnlyList<SiteSummaryReportDto> SummaryList { get; set; } = [];

        [BindProperty]
        public SiteSummaryQuerySpec.SiteSummaryExportTo SiteSummaryExportTo { get; set; }


        public async Task<IActionResult> OnGetAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;

            SummaryList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;

            SiteSummaryExportTo = Spec.ExportTo;

            switch (SiteSummaryExportTo)
            {
                case SiteSummaryQuerySpec.SiteSummaryExportTo.Storage:
                    if (!await ExportToStorage())
                    {
                        GetReturnMessage(ResponseType.Storage);
                        return Page();
                    }
                    break;
                case SiteSummaryQuerySpec.SiteSummaryExportTo.SharePoint:
                    if (!await ExportToSharePoint())
                    {
                        GetReturnMessage(ResponseType.SharePoint);
                        return Page();
                    }
                    break;
                case SiteSummaryQuerySpec.SiteSummaryExportTo.Local:
                    if (!await ExportLocal())
                    {
                        GetReturnMessage(ResponseType.Local);
                        return Page();
                    }
                    break;
                default:
                    break;
            }
            GetReturnMessage(ResponseType.Success);
            return Page();
        }

        public async Task<bool> ExportToStorage()
        {
            SummaryList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return true;
        }

        public async Task<bool> ExportToSharePoint()
        {
            SummaryList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return true;
        }

        public async Task<bool> ExportLocal()
        {
            SummaryList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return true;
        }

        public async Task<IActionResult> OnPostReportAsync()
        {
            var fileName = $"Events_NoActionTaken_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsNoActionTakenList" List to go to a report
            IList<EventsNoActionTakenReportDto> eventsList = await _repository.GetEventsNoActionTakenReportAsync();

            // Map to EventsNoActionTakenReportDto
            var eventsNoActionTakenReportList = from p in eventsList
                                                select new EventsNoActionTakenReportDto(p);

            // Export to Excel
            return File(
                eventsNoActionTakenReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventNoActionTaken),
                "application/vnd.ms-excel", fileName);
        }

        public string GetReturnMessage(ResponseType responseType) => responseType switch
        {
            ResponseType.Storage => "Writing PDF Files to Storage was unsuccessful",
            ResponseType.SharePoint => "Writing PDF Files to SharePoint was unsuccessful",
            ResponseType.Local => "Exporting single PDF file was unsuccessful",
            ResponseType.Success => "Operation completed successfully",
            _ => ""
        };

        public enum ResponseType
        {
            Storage,
            SharePoint,
            Local,
            Success
        }
    }
}
