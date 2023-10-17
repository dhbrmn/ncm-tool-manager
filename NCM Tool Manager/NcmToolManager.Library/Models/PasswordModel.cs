namespace NcmToolManager.Library.Models
{
    public class PasswordModel
    {
        public string Password { get; set; }
        public string Salt { get; set; }

        public PasswordModel(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
