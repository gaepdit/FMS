using System.Numerics;
using System.Reflection.Metadata;

namespace FMS.Helpers
{
    public static class UrlHelper
    {
        public static string GetWorkingFolderLink(string hsiNumber)
        {
            return hsiNumber == string.Empty
                ? string.Empty
                : string.Concat("https://gets.sharepoint.com/sites/ResponseandRemediationProgram/HSI/Shared%20Documents/", hsiNumber, "/Working-Docs");
        }
    }
}
