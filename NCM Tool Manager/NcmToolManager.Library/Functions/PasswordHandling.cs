using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Models;
using System;
using System.Security.Cryptography;

namespace NcmToolManager.Library.Functions
{
    public class PasswordHandling
    {
        private const int SaltSize = 16;
        private const int HashSize = 16;
        private const int Iterations = 10000;

        public static PasswordModel EncryptPassword(string plainPassword)
        {
            byte[] salt = GenerateSalt();
            byte[] hash = ComputeHash(plainPassword, salt);

            return new PasswordModel(Convert.ToBase64String(hash), Convert.ToBase64String(salt)); ;
        }
        public static bool VerifyUser( string username )
        {
            try
            {
                UserModel user = SqlServerAccess.ReadUserFromDb(new UserModel { UserName = username });
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool VerifyPassword( string username, string password )
        {
            
            UserModel user = new UserModel { UserName = username };
            UserModel fullUser;
            try
            {
                fullUser = SqlServerAccess.ReadUserFromDb(user);
            }
            catch
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(fullUser.Salt);
            byte[] storedHash = Convert.FromBase64String(fullUser.Password);
            byte[] inputHash = ComputeHash(password, salt);

            return SlowEquals(storedHash, inputHash);
        }
        private static byte[] GenerateSalt()
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                 byte[] salt = new byte[SaltSize];
                 rng.GetBytes(salt);
                return salt;
            }
        }

        private static byte[] ComputeHash(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                byte[] result = new byte[SaltSize + HashSize];
                Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
                Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

                return result;
            }
        }
        private static bool SlowEquals(byte[] storedHash, byte[] inputHash)
        {
            int diff = storedHash.Length ^ inputHash.Length;

            for (int i = 0; i < storedHash.Length && i < inputHash.Length; i++)
            {
                diff |= storedHash[i] ^ inputHash[i];
            }

            return diff == 0;
        }
    }
}
