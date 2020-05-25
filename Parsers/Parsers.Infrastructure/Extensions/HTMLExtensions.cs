using System;
using System.Text.RegularExpressions;

namespace Parsers.Infrastructure
{
    /// <summary>
    /// Extensions for fluent work with HTML
    /// </summary>
    public static class HTMLExtensions
    {
        /// <summary>
        /// Delete useless tags from <paramref name="this"/>
        /// </summary>
        /// <param name="this">Target string, that contains HTML tags</param>
        /// <returns>
        ///     String without HTML tags
        /// </returns>
        public static string StripHTML(this string @this)
        {
            if (@this != null)
            {
                string res = Regex.Replace(@this, "<.*?>", String.Empty);
                res = Regex.Replace(res, "\r", String.Empty);
                res = Regex.Replace(res, "\n", String.Empty);
                res = Regex.Replace(res, "\t", String.Empty);
                res = Regex.Replace(res, "&quot;", String.Empty);
                res = Regex.Replace(res, "&lt;", String.Empty);
                res = Regex.Replace(res, "br&gt;", String.Empty);
                return res;
            }

            return string.Empty;
        }
    }
}
