using System.Numerics;
using System.Reflection.Metadata;
using FMS.Platform;
using FMS.Platform.Extensions;

namespace FMS.Helpers
{
    public static class UrlHelper
    {
        public static string GetHSIFolderLink(string hsiNumber)
        {
            return hsiNumber == string.Empty
                ? string.Empty
                : string.Concat(GlobalConstants.HSIFolder, hsiNumber);
        }

        public static string GetNotificationFolderLink(string notificationId)
        {
            return notificationId == string.Empty
                ? string.Empty
                : string.Concat(GlobalConstants.NotificationFolder, notificationId.Substring(2));
        }
    }
}
