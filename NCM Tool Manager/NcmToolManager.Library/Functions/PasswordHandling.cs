using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Functions
{
    public class PasswordHandling
    {
        /// <summary>
        /// Generator for random password salts
        /// </summary>
        /// <returns>Returns randomized salt value for password encryption</returns>
        private static byte[] GenerateSalt()
        {
            Random rnd = new Random();
            byte[] salt = new byte[16];
            rnd.NextBytes(salt);
            return salt;
        }
        /// <summary>
        /// Method to encrypt a string password into a hash form
        /// </summary>
        /// <param name="stringPassword">String of input password to be encrypted</param>
        /// <param name="salt">Output string of random generated salt parameter to store in db</param>
        /// <returns>Returns hash encrypted password</returns>
        public static string EncryptPassword(string stringPassword, out string salt)
        {
            byte[] saltByte = GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(stringPassword, saltByte, 10000);
            byte[] hashByte = pbkdf2.GetBytes(16);
            byte[] passByte = new byte[32];
            Array.Copy(saltByte, 0, passByte, 0, 16);
            Array.Copy(hashByte, 0, passByte, 16, 16);
            salt = Convert.ToBase64String(saltByte);
            return Convert.ToBase64String(passByte);
        }
        /// <summary>
        /// Verifies if the input password is the same as the stored password
        /// </summary>
        /// <param name="password">String as the input password</param>
        /// <returns>Returns boolean if the passwords match</returns>
        public static bool VerifyPassword(string password)
        {
            bool isMatch = false;
            byte[] salt = Convert.FromBase64String(GetUserSalt());
            byte[] pass = Convert.FromBase64String(GetUserPass());
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hashByte = pbkdf2.GetBytes(16);
            byte[] passByte = new byte[32];
            Array.Copy(salt, 0, passByte, 0, 16);
            Array.Copy(hashByte, 0, passByte, 16, 16);
            if (passByte == pass)
            {
                isMatch = true;
            }
            return isMatch;
        }

    }
}
