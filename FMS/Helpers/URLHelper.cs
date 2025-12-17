using FMS.Platform.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace FMS.Helpers
{
    public static class UrlHelper
    {
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

        public static string GetMapLink(decimal lat, decimal lon)
        {
            var link = string.Concat(GlobalConstants.MapCoordLink, lat.ToString(), ",", lon.ToString());
            return link;
        }
    }
}
