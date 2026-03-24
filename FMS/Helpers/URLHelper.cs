using FMS.Domain.Dto;
using FMS.Platform.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace FMS.Helpers
{
    public class UrlHelper
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public UrlHelper(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GoogleMapsApiKey => _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;

        public static string GetHSIFolderLink(string hsiNumber)
        {
            return string.IsNullOrEmpty(hsiNumber)
                ? null 
                : string.Concat(GlobalConstants.HSIFolder, hsiNumber);
        }

        public static string GetNotificationFolderLink(string notificationId)
        {
            return string.IsNullOrEmpty(notificationId)
                ? null
                : string.Concat(GlobalConstants.NotificationFolder, notificationId.Substring(2));
        }

        public static string GetPendingNotificationFolderLink(string notificationId)
        {
            return string.IsNullOrEmpty(notificationId)
                ? null
                : string.Concat(GlobalConstants.PendingNotificationFolder, notificationId.Substring(2));
        }

        public static string GetComplaintsFolderLink(string notificationId)
        {
            return string.IsNullOrEmpty(notificationId)
                ? null
                : string.Concat(GlobalConstants.ComplaintsFolder, notificationId.Substring(2));
        }

        public static string GetMapLink(decimal lat, decimal lon)
        {
            var link = string.Concat(GlobalConstants.MapCoordLink, lat.ToString(), ",", lon.ToString());
            return link;
        }

        public string GetGoogleMapsUrlLink(decimal? lat, decimal? lon, string mapZoom, string mapType)
        {
            if (lat != 0 && lon != 0)
            {
                return $"https://maps.googleapis.com/maps/api/staticmap?center={lat},{lon}&zoom={mapZoom}&size=250x250&markers=size:mid|color:red|{lat},{lon}&maptype={mapType}&key={GoogleMapsApiKey}&style=feature:poi|visibility:off";
            }
            return null;
        }
    }
}
