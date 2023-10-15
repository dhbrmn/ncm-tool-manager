using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class SalesPersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public SalesPersonModel() { }
        public SalesPersonModel(string name, string lastName, string email, string phone, string address, string city, string country)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
    }
}
