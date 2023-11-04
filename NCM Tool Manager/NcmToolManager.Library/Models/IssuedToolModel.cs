using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class IssuedToolModel
    {
        public string Name
        {
        get; set; 
        }
        public int Quantity
        {
        get; set; 
        }
        public IssuedToolModel(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
