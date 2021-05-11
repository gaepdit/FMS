using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FMS.Platform.Extensions
{
    public static class TempDataExtensions
    {
        private static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        private static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out var o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        public static void SetDisplayMessage(this ITempDataDictionary tempData, Context context, string message)
        {
            tempData.Set(nameof(DisplayMessage), new DisplayMessage(context, message));
        }

        public static DisplayMessage GetDisplayMessage(this ITempDataDictionary tempData)
        {
            return tempData.Get<DisplayMessage>(nameof(DisplayMessage));
        }
    }
}
