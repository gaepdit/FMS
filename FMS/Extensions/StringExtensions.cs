using System.Diagnostics;
using System.Linq;

namespace FMS
{
    public static class StringExtensions
    {
        public static string MD5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return string.Join("", System.Security.Cryptography.MD5.Create()
                .ComputeHash(System.Text.Encoding.ASCII.GetBytes(value.Trim().ToLower()))
                .Select(s => s.ToString("x2")));
        }

        [DebuggerStepThrough]
        public static string ForceToString(this object input) =>
            input == null || string.IsNullOrEmpty(input.ToString()) ? "" : input.ToString();
    }
}
