using FMS.Platform.Extensions;

namespace FMS.Helpers
{
    public class SiteMapHelper
    {
        public static string GetSiteSummaryUrl(string facilityNumber)
        {
            // Determine the environment and return the appropriate URL
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            return environment switch
            {
                "Development" => GlobalConstants.MapDirectory,
                "UAT" => GlobalConstants.MapDirectory,
                _ => GlobalConstants.MapDirectory,
            };
        }
    }
}
