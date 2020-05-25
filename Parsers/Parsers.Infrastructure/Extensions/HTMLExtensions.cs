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
                string res = Regex.Replace(@this, "<.*?>", string.Empty);
                res = Regex.Replace(res, "&.*?;", string.Empty);
                res = Regex.Replace(res, "\r", string.Empty);
                res = Regex.Replace(res, "\n", string.Empty);
                res = Regex.Replace(res, "\t", string.Empty);
                res = Regex.Replace(res, "<br>", "\r\n");
                return res;
            }

            return string.Empty;
        }
    }
}
