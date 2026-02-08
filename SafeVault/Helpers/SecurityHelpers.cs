using System.Text.RegularExpressions;

namespace SafeVault.Helpers
{
    public static class SecurityHelpers
    {
        /// <summary>
        /// Sanitizes input by removing common XSS attack patterns.
        /// This is SafeVault's defensive layer for any user-supplied text.
        /// </summary>
        public static string SanitizeXSS(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string sanitized = input;

            // Remove <script>...</script>
            sanitized = Regex.Replace(
                sanitized,
                "<script.*?>.*?</script>",
                "",
                RegexOptions.IgnoreCase | RegexOptions.Singleline
            );

            // Remove all HTML tags
            sanitized = Regex.Replace(
                sanitized,
                "<.*?>",
                "",
                RegexOptions.IgnoreCase
            );

            // Remove javascript: URLs
            sanitized = Regex.Replace(
                sanitized,
                "javascript:",
                "",
                RegexOptions.IgnoreCase
            );

            // Remove event handlers like onclick=, onload=, etc.
            sanitized = Regex.Replace(
                sanitized,
                @"on\w+\s*=",
                "",
                RegexOptions.IgnoreCase
            );

            // Trim whitespace
            sanitized = sanitized.Trim();

            return sanitized;
        }

        /// <summary>
        /// Checks if input contains unsafe XSS patterns.
        /// Returns true if the input is safe.
        /// </summary>
        public static bool IsValidXSSInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;

            // Detect <script> tags
            if (Regex.IsMatch(input, "<script.*?>", RegexOptions.IgnoreCase))
                return false;

            // Detect any HTML tags
            if (Regex.IsMatch(input, "<.*?>"))
                return false;

            // Detect javascript: URLs
            if (Regex.IsMatch(input, "javascript:", RegexOptions.IgnoreCase))
                return false;

            // Detect event handlers like onclick=
            if (Regex.IsMatch(input, @"on\w+\s*=", RegexOptions.IgnoreCase))
                return false;

            return true;
        }
    }
}