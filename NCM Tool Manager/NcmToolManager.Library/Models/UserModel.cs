using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class UserModel
    {
        private int _id;
        private string _name;
        private string _lastName;
        private string _userName;
        private string _password;
        private string _salt;
        private int _role;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public string Salt { get => _salt; set => _salt = value; }
        public int Role { get => _role; set => _role = value; }

        public UserModel() { }
        public UserModel(string name, int role) 
        { 
            _name = name;
            _role = role;
        }
        public UserModel( string userName, string password )
        {
            _userName = userName;
            _password = password;
        }
        public UserModel(string name, string lastName, int role)
        {
            _name = name;
            _lastName = lastName;
            _role = role;
        }
        public UserModel(string name, string userName, string password, int role)
        {
            _name = name;
            _userName = userName;
            _password = password;
            _role = role;
        }
    }
}
