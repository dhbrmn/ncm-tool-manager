using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Functions
{
    public class PasswordEncription
    {
        public byte[] GenerateSalt()
        {
            Random rnd = new Random();
            byte[] salt = new byte[32];
            rnd.NextBytes(salt);
            return salt;
        }
        public string EncryptPassword(string stringPassword, out string salt)
        {
            byte[] saltByte = GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(stringPassword, saltByte, 10000);
            byte[] hashByte = pbkdf2.GetBytes(32);
            byte[] passByte = new byte[64];
            Array.Copy(saltByte, 0, passByte, 0, 32);
            Array.Copy(hashByte, 0, passByte, 32, 32);
            salt = Convert.ToBase64String(saltByte);
            return Convert.ToBase64String(passByte);
        }
    }
}
