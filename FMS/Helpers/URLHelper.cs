using System.Numerics;

namespace FMS.Helpers
{
    public class URLHelper
    {
        public static string GetTemplateFolderLink() => "https://gets.sharepoint.com/sites/RRP-Templates";
        public static string GetWorkingFolderLink(string hsiNumber) => string.Concat("https://gets.sharepoint.com/sites/ResponseandRemediationProgram/HSI/Shared%20Documents/", hsiNumber, "/Working-Docs");
    }
}
