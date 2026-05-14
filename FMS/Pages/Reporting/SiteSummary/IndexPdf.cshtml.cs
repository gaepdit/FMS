using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Platform.Extensions;
using iText.Html2pdf;
using iText.Signatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class IndexPdfModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly IConfiguration _configuration;

        private static readonly HttpClient client = new HttpClient();

        public IndexPdfModel(IReportingRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public DisplayMessage Message { get; private set; }


        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        [BindProperty]
        public IReadOnlyList<SiteSummaryReportDto> SummaryList { get; set; } = [];

        [BindProperty]
        public SiteSummaryQuerySpec.SiteSummaryExportTo SiteSummaryExportTo { get; set; }


        public async Task<IActionResult> OnGetAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;
            Spec.IsPdf = true;

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
                    await ExportLocal();
                    TempData?.SetDisplayMessage(Context.Success, GetReturnMessage(ResponseType.Local));
                    return Page();
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
            try
            {
                var fileName = $"SiteSummary_{Spec.FacilityNumber}.pdf";

                string htmlSrc = "https://localhost:44362/Reporting/SiteSummary/Report/" + Spec.FacilityNumber;

                using Stream inputStream = await client.GetStreamAsync(htmlSrc);

                using (FileStream pdfDest = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {

                    ConverterProperties properties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(inputStream, pdfDest, properties);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

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
