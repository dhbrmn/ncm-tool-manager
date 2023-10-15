﻿using System;
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
        public LoginModel(string userName, PasswordModel passMod, int role)
        {
            UserName = userName;
            Password = passMod.Password;
            Salt = passMod.Salt;
            Role = role;
        }

    }
}
