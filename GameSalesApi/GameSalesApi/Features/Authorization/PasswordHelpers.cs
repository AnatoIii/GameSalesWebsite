using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GameSalesApi.Features.Authorization
{
    /// <summary>
    /// Holds functionality to work with passwords
    /// </summary>
    public static class PasswordHelpers
    {
        private static readonly Regex _prHasNumber = new Regex(@"[0-9]+");
        private static readonly Regex _prHasUpperChar = new Regex(@"[A-Z]+");
        private static readonly Regex _prHasMiniMaxChars = new Regex(@".{8,15}");
        private static readonly Regex _prHasLowerChar = new Regex(@"[a-z]+");
        private static readonly Regex _prHasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        /// <summary>
        /// Generate Random Salt
        /// </summary>
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Hashes the password with the given salt
        /// </summary>
        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, 
                salt: salt, 
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <summary>
        /// Validates that the given password and salt are hashed to the specified hash
        /// </summary>
        public static bool ValidatePassword(string password, string salt, string hash)
        {
            return HashPassword(password, Convert.FromBase64String(salt)) == hash;
        }

        /// <summary>
        /// Check if password satisfies the inner rules
        /// </summary>
        /// <param name="password">Target password</param>
        /// <param name="errorMessage">Out error message if some check was failed</param>
        /// <returns>
        ///     true - if password correct
        ///     false - if some check was failed. Fill <paramref name="errorMessage"/> with a description 
        ///     of what was wrong in the <paramref name="password"/>
        /// </returns>
        public static bool IsPasswordSatisfied(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password should not be empty");

            if (!_prHasLowerChar.IsMatch(password))
            {
                errorMessage = "Password should contain at least one lower case letter.";
                return false;
            }
            else if (!_prHasUpperChar.IsMatch(password))
            {
                errorMessage = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!_prHasMiniMaxChars.IsMatch(password))
            {
                errorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
                return false;
            }
            else if (!_prHasNumber.IsMatch(password))
            {
                errorMessage = "Password should contain at least one numeric value.";
                return false;
            }
            else if (!_prHasSymbols.IsMatch(password))
            {
                errorMessage = "Password should contain at least one special case character.";
                return false;
            }

            return true;
        }
    }
}
