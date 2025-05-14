using Microsoft.AspNetCore.DataProtection;

namespace Backend.Helpers
{
    public class PasswordHelper
    {
        private readonly IDataProtector _protector;

        public PasswordHelper(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("ConnectionStringsProtector");
        }

        public string Encrypt(string plainText)
        {
            return _protector.Protect(plainText);
        }

        public string Decrypt(string encryptedText)
        {
            return _protector.Unprotect(encryptedText);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
