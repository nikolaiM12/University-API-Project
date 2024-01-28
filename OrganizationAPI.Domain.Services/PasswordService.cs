using OrganizationAPI.Domain.Abstractions.Services;
using System.Security.Cryptography;

namespace OrganizationAPI.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password, out string salt)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[32];
                rng.GetBytes(saltBytes);

                salt = Convert.ToBase64String(saltBytes);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA512))
            {
                byte[] hash = pbkdf2.GetBytes(64);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            byte[] storedHash = Convert.FromBase64String(hashedPassword);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA512))
            {
                byte[] inputHash = pbkdf2.GetBytes(storedHash.Length);

                return storedHash.SequenceEqual(inputHash);
            }
        }
    }
}
