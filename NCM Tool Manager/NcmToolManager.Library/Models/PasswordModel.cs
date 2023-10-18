using NcmToolManager.Library.Functions;

namespace NcmToolManager.Library.Models
{
    public class PasswordModel
    {
        public string Password { get; set; }
        public string Salt { get; set; }

        public PasswordModel( string password )
        {
            var output = PasswordHandling.EncryptPassword( password );
            Password = output.Password;
            Salt = output.Salt;
        }
        public PasswordModel(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
