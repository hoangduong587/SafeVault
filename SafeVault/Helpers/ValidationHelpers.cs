    using System.Text.RegularExpressions;

    namespace SafeVault.Helpers
    {
        public static class ValidationHelpers
        {
            /// <summary>
            /// Validates a username. Allowed characters: letters, numbers, @, #, $.
            /// </summary>
            public static bool ValidateUsername(string username, out string errorMessage)
            {
                errorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(username))
                {
                    errorMessage = "Username cannot be empty.";
                    return false;
                }

                if (username.Length < 3 || username.Length > 50)
                {
                    errorMessage = "Username must be between 3 and 50 characters.";
                    return false;
                }

                // Allowed: A–Z, a–z, 0–9, @, #, $
                var regex = new Regex("^[a-zA-Z0-9@#$]+$");
                if (!regex.IsMatch(username))
                {
                    errorMessage = "Username can only contain letters, numbers, @, #, $.";
                    return false;
                }

                return true;
            }

            /// <summary>
            /// Validates a password. Must be 8–128 characters and not whitespace-only.
            /// </summary>
            public static bool ValidatePassword(string password, out string errorMessage)
            {
                errorMessage = string.Empty;

                if (string.IsNullOrWhiteSpace(password))
                {
                    errorMessage = "Password cannot be empty or whitespace.";
                    return false;
                }

                if (password.Length < 8)
                {
                    errorMessage = "Password must be at least 8 characters long.";
                    return false;
                }

                if (password.Length > 128)
                {
                    errorMessage = "Password must be shorter than 128 characters.";
                    return false;
                }

                return true;
            }
        }
    }