using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class ToolModel
    {
        //Variables
        private int _id;
        private string _name;
        private readonly SerialModel _serial;
        private ManufacturerModel _manufacturer;
        private SellerModel _seller;

        //Fields
        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public SerialModel Serial { get => _serial; }
        public ManufacturerModel Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public SellerModel Seller { get => _seller; set => _seller = value; }
        //Contructors
        public ToolModel(string name)
        {
            _name = name;
            _serial = new SerialModel();
        }
        public ToolModel(string name, ManufacturerModel manufacturer)
        {
            _name = name;
            _manufacturer = manufacturer;
            _serial = new SerialModel();
        }
        public ToolModel(string name, ManufacturerModel manufacturer, SellerModel seller)
        {
            _name = name;
            _manufacturer = manufacturer;
            _seller = seller;
            _serial = new SerialModel();
        }

    }
}
