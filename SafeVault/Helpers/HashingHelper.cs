using System.Security.Cryptography;
using System.Text;

namespace SafeVault.Helpers
{
    public static class HashingHelper
    {
        // You can tweak these if needed
        private const int SaltSize = 16;      // 128-bit
        private const int KeySize = 32;       // 256-bit
        private const int Iterations = 100_000;

        /// <summary>
        /// Hashes a password using PBKDF2 with a random salt.
        /// Returns a Base64 string containing salt + hash.
        /// </summary>
        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(KeySize);

            // Combine salt + key into one array
            byte[] result = new byte[SaltSize + KeySize];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(key, 0, result, SaltSize, KeySize);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Verifies a password against a stored Base64(salt+hash) string.
        /// </summary>
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(storedHash))
                return false;

            byte[] storedBytes;

            try
            {
                storedBytes = Convert.FromBase64String(storedHash);
            }
            catch
            {
                // Not a valid Base64 string
                return false;
            }

            if (storedBytes.Length != SaltSize + KeySize)
                return false;

            byte[] salt = new byte[SaltSize];
            byte[] storedKey = new byte[KeySize];

            Buffer.BlockCopy(storedBytes, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(storedBytes, SaltSize, storedKey, 0, KeySize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedKey = pbkdf2.GetBytes(KeySize);

            // Constant-time comparison
            return CryptographicOperations.FixedTimeEquals(storedKey, computedKey);
        }
    }
}