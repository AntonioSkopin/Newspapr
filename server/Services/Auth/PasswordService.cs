using System;
using System.Security.Cryptography;
using System.Text;

namespace server.Services.Auth
{
    public class PasswordService
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Please provide a password.");

            // Encrypt password
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Please provide a password.");

            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).");

            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).");

            // Compare stored salt with salt created with provided password
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    // Compare each byte and checks if it equals stored hash 
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}