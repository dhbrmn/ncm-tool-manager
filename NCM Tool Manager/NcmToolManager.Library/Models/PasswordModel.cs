using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class PasswordModel
    {
        private string _password;
        private string _salt;

        public string Password
        {
            get => _password;
            set => _password = value;
        }
        public string Salt
        {
            get => _salt;
            set => _salt =  value ;
        }
        public PasswordModel()
        {
        }
        public PasswordModel( string password, string salt )
        {
            Password = password;
            Salt = salt;
        }
    }
}
