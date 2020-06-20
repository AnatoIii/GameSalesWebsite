namespace System.Net
{
    /// <summary>
    /// Helper class for wotk with URL
    /// </summary>
    public static class URLHelpers
    {
        /// <summary>
        /// Substitutes in target URL params count by <paramref name="count"/> and offset by <paramref name="offset"/>
        /// </summary>
        /// <param name="this">Target link</param>
        /// <param name="count">Target count</param>
        /// <param name="offset">Target offset</param>
        /// <exception cref="ArgumentNullException">If <paramref name="this"/> is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> less than 1</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="offset"/> less than 0</exception>
        public static string HandleURL(this string @this, int count, int offset)
        {
            if (string.IsNullOrWhiteSpace(@this))
                throw new ArgumentNullException("Target link can`t be null or empty");

            if (count < 1)
                throw new ArgumentOutOfRangeException("Count can`t be less than 1");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("Offset can`t be less than 0");

            return @this.Replace("@SIZE", count.ToString())
                .Replace("@START", offset.ToString());
        }
    }
}
