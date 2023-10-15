using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SellerId { get; set; }

        public ManufacturerModel(string name)
        {
            Name = name;
        }
    }
}
