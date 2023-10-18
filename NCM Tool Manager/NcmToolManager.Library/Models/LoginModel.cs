using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Role { get; set; }

        public LoginModel() { }
        public LoginModel( string userName )
        {
            UserName = userName;
        }
        public LoginModel (string userName, string password )
        {
            UserName = userName;
            var hashOutput = new PasswordModel( password );
            Password = hashOutput.Password;
            Salt = hashOutput.Salt;
            Role = 0;
        }
        public LoginModel(string userName, string password, string salt, int role)
        {
            UserName = userName;
            Password = password;
            Salt = salt;
            Role = role;
        }

    }
}
