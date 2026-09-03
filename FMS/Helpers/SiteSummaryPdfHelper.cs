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

        /// <summary>
        /// Converts a simple HTML file to PDF using an InputStream and an OutputStream as arguments for the convertToPdf() method.
        /// </summary>

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
        public string ADDRESS = "~/Reporting/SiteSummary/Report/";

        /// <summary>
        /// The main method of this example.
        /// </summary>
        /// <param name="args">no arguments are needed to run this example.</param>
        public static void CreateNewPdf(String hsiNumber)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();

            new SiteSummaryPdfHelper().CreatePdf(new Uri(new SiteSummaryPdfHelper().ADDRESS + hsiNumber), DEST);
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
