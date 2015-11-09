using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Api.Security
{
    public class PasswordHelper
    {
        public static string GenerateSalt(int byteLength)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[byteLength];
            rng.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public static string HashPassword(string pass, string salt)
        {
            var saltedPassword = string.Concat(salt, pass);
            byte[] buffer = Encoding.UTF8.GetBytes(saltedPassword);
            SHA1CryptoServiceProvider cryptoTransform = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransform.ComputeHash(buffer)).Replace("-", "");
        }

        public static bool Verify(string hash, string salt, string password)
        {
            var hashAttempt = HashPassword(password, salt);
            return hash == hashAttempt;
        }
    }
}