using Dapper;
using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Models;
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

        private static byte[] GenerateSalt()
        {
            Random rnd = new ();
            byte[] salt = new byte[16];
            rnd.NextBytes(salt);
            return salt;
        }

        public static PasswordModel EncryptPassword(string stringPassword)
        {
            byte[] saltByte = GenerateSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(stringPassword, saltByte, 10000);
            byte[] hashByte = pbkdf2.GetBytes(16);
            byte[] passByte = new byte[32];
            Array.Copy(saltByte, 0, passByte, 0, 16);
            Array.Copy(hashByte, 0, passByte, 16, 16);
            PasswordModel passwordModel = new(Convert.ToBase64String(passByte), Convert.ToBase64String(saltByte));
            return passwordModel;
        }
        public static bool VerifyUser( string username )
        {
            bool isMatch = false;
            UserModel user = new();
            user.UserName = username;
            try
            {
                user = SqlServerAccess.ReadUserFromDb(user);
            }
            catch
            {
                return isMatch;
            }
            isMatch = true;
            return isMatch;
        }
        public static bool VerifyPassword( string username, string password )
        {
            
            bool isMatch = false;
            UserModel user = new(username, password);

            UserModel fullUser = SqlServerAccess.ReadUserFromDb(user);

            byte[] salt = Convert.FromBase64String(fullUser.Salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hashByte = pbkdf2.GetBytes(16);
            byte[] passByte = new byte[32];
            Array.Copy(salt, 0, passByte, 0, 16);
            Array.Copy(hashByte, 0, passByte, 16, 16);
            string passString = Convert.ToBase64String(passByte);

            if (passString.Equals(fullUser.Password))
            {
                isMatch = true;
            }
            return isMatch;
        }

    }
}
