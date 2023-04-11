using System.Text;
using XSystem.Security.Cryptography;

namespace AuthenDemo.Models.Utils
{
    public static class Utils
    {
        public static String ComputeSha256Hash(this string username,  string password, string salt)
        {
            var encrypt = new SHA256Managed();
            var hash = new StringBuilder();
            var crypto = encrypt.ComputeHash(Encoding.UTF8.GetBytes($"{username} - {salt} - {password}"));

            foreach (var theByte in crypto) hash.Append(theByte.ToString("x2"));

            return hash.ToString();
        }

        public static bool ValidPassword(this string username, string password, string salt, string passHash)
        {
            var isValid = ComputeSha256Hash(username, password, salt).Equals(passHash);

            return isValid;
        }
    }
}
