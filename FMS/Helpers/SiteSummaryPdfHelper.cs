using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using iText.Commons.Utils;
using System.Net;
using System;
using System.IO;
using iText.Html2pdf;

namespace FMS.Helpers
{
    public class SiteSummaryPdfHelper
    {
        private readonly IReportingRepository _repository;
        private readonly IConfiguration _configuration;
        public SiteSummaryPdfHelper(IReportingRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public string GoogleMapsApiKey => _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;

        public string GetGoogleMapsUrl(SiteSummaryReportDto facility)
        {
            if (facility.Latitude != 0 && facility.Longitude != 0 && facility.LocationDetails != null)
            {
                return
                    $"https://maps.googleapis.com/maps/api/staticmap?center={facility.Latitude},{facility.Longitude}&zoom={facility.LocationDetails?.MapZoom}&size=250x250&markers=color:red|{facility.Latitude},{facility.Longitude}&maptype=roadmap&key={GoogleMapsApiKey}&style=feature:poi|visibility:off";
            }
            return null;
        }

        public string GetStatusLanguage(SiteSummaryReportDto facility) =>
            SiteSummaryHelper.GetCleanupStatusLanguage(facility);

        public string GetScoreLanguage(SiteSummaryReportDto facility)
        {
            var groundWaterLang = SiteSummaryHelper.GetLanguageForGWScore(facility);
            var onsiteScoreLang = SiteSummaryHelper.GetLanguageForOSScore(facility);
            var exLang = "";
            if (facility.ScoreDetails != null && facility.ScoreDetails.UseComments)
            {
                exLang = SiteSummaryHelper.GetLanguageForExceptions(facility);
            }

            return groundWaterLang + onsiteScoreLang + exLang;
        }

        public bool HasSublistedParcels(SiteSummaryReportDto facility)
        {
            foreach (var parcel in facility.Parcels)
            {
                if (parcel.ParcelType?.Name == "SubList")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Converts a simple HTML file to PDF using an InputStream and an OutputStream as arguments for the convertToPdf() method.
        /// </summary>
        public class CreateFromURL
        {
            const string USER_AGENT = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; WOW64; " +
                                      "Trident/4.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; " +
                                      ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET CLR 3.0.30618; " +
                                      "InfoPath.2; OfficeLiveConnector.1.3; OfficeLivePatch.0.0)";

            /// <summary>
            /// The path to the resulting PDF file.
            /// </summary>
            public static readonly String DEST = String.Concat(Environment.GetEnvironmentVariable("PdfFilePath"), "url2pdf_1.pdf");

            /// <summary>
            /// The target folder for the result.
            /// </summary>
            public static readonly String ADDRESS = "https://stackoverflow.com/help/on-topic";

            /// <summary>
            /// The main method of this example.
            /// </summary>
            /// <param name="args">no arguments are needed to run this example.</param>
            public static void Main(String[] args)
            {
                //String licensePath = LicenseUtil.GetPathToLicenseFileWithITextCoreAndPdfHtmlAndPdfCalligraphProducts();
                //using (Stream license = FileUtil.GetInputStreamForFile(licensePath))
                //{
                //    LicenseKey.LoadLicenseFile(license);
                //}

                FileInfo file = new FileInfo(DEST);
                file.Directory.Create();

                new CreateFromURL().CreatePdf(new Uri(ADDRESS), DEST);
            }

            /// <summary>
            /// Creates the PDF file.
            /// </summary>
            /// <param name="url">the URL object for the web page</param>
            /// <param name="dest">the path to the resulting PDF</param>
            public void CreatePdf(Uri url, String dest)
            {
                using (var fileStream = new FileStream(dest, FileMode.Create))
                {
                    try
                    {
                        using (var httpClient = new System.Net.Http.HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(15);
                            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);
                            var websiteTask = httpClient.GetByteArrayAsync(url);
                            websiteTask.Wait();
                            byte[] website = websiteTask.Result;
                            HtmlConverter.ConvertToPdf(new MemoryStream(website), fileStream);
                        }
                    }
                    catch (AggregateException ae) when (ae.InnerException is System.Net.Http.HttpRequestException hre)
                    {
                        // Handle HTTP request exception
                    }
                    catch (System.Net.Http.HttpRequestException hre)
                    {
                        // Handle HTTP request exception
                    }
                    catch (System.TimeoutException)
                    {
                        // Handle timeout
                    }
                }
            }
        }
    }
}
