using FMS.Platform.Extensions;

namespace FMS.Helpers
{
    public class SiteMapHelper
    {
        private readonly string MapDirectory = Environment.GetEnvironmentVariable("MAP_DIR") ?? "/Reporting/Map/";
        public string GetSiteMap(string facilityNumber)
        {
            // Determine the environment and return the appropriate URL
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            return environment switch
            {
                "Development" => MapDirectory,
                "UAT" => MapDirectory,
                _ => MapDirectory,
            };
        }
    }
}
