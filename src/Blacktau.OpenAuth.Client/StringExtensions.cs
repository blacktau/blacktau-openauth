namespace Blacktau.OpenAuth
{
    using System;

    public static class StringExtensions
    {
        public static string UrlEncode(this string input)
        {
            return Uri.EscapeDataString(input);
        }
    }
}