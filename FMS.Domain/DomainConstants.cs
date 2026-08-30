namespace FMS.Domain
{
    public static class DomainConstants
    {
        public const string SiteSummaryReportPath = "/Reporting/SiteSummary/Report/";

        private const string siteSummaryReportPathPdfDev = "https://dev-fms.gaepd.org/Reporting/SiteSummary/2026/";

        private const string siteSummaryReportPathPdfProd = "https://fms.gaepd.org/Reporting/SiteSummary/2026/";

        private const string siteSummaryReportPathPdfUat = "https://uat-fms.gaepd.org/Reporting/SiteSummary/2026/";

        public const string pdfSuffix = ".pdf";

        public static string SiteSummaryReportPathPdfDev => siteSummaryReportPathPdfDev;

        public static string SiteSummaryReportPathPdfProd => siteSummaryReportPathPdfProd;

        public static string SiteSummaryReportPathPdfUat => siteSummaryReportPathPdfUat;
    }
}
