using FMS.Platform.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace FMS.Helpers
{
    public static class UrlHelper
    {
        public static string GetHSIFolderLink(string hsiNumber)
        {
            return hsiNumber.IsNullOrEmpty()
                ? null 
                : string.Concat(GlobalConstants.HSIFolder, hsiNumber);
        }

        public static string GetNotificationFolderLink(string notificationId)
        {
            return notificationId.IsNullOrEmpty()
                ? null
                : string.Concat(GlobalConstants.NotificationFolder, notificationId.Substring(2));
        }

        public static string GetPendingNotificationFolderLink(string notificationId)
        {
            return notificationId.IsNullOrEmpty()
                ? null
                : string.Concat(GlobalConstants.PendingNotificationFolder, notificationId.Substring(2));
        }
    }
}
