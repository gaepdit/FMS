using FMS.Platform.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Net;

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


        /// Checks the Sharepoint Site exists or not.
        ///
        /// Param: The URL of the Sharepoint Site.
        /// Returns: URL If the Site exists. Null if Site does not exist
        private static string RemoteSiteExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "GET";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns The URL if the Status code == 200
                response.Close();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return url;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                //Any exception will returns false.
                return null;
            }
        }
    }
}
