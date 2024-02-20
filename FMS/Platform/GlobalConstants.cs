namespace FMS.Platform.Extensions
{
    // App-wide global variables
    public static class GlobalConstants
    {
        // Default pagination size for search results, etc.
        public const int PageSize = 25;

        // Link to Template folder in Sharepoint
        public const string TemplateFolderLink = "https://gets.sharepoint.com/sites/RRP-Templates";

        // Link to HSI folder
        public const string HSIFolder = "https://gets.sharepoint.com/sites/ResponseandRemediationProgram/HSI/Shared%20Documents/";

        // Link to Notifications folder
        public const string NotificationFolder = "https://gets.sharepoint.com/sites/ResponseandRemediationProgram/RNs/Shared%20Documents/";

        // Link to Notifications folder
        public const string PendingNotificationFolder = "https://gets.sharepoint.com/sites/ResponseandRemediationProgram/RNs/Pending%20RNs/";

        public const string RNFacilityNamePattern = @"\A(RN\d+?)\b";
    }
}
