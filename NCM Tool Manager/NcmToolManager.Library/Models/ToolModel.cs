using NcmToolManager.Library.Models;

namespace NcmToolManager.Library.Models
{
    public class ToolModel
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ManufacturerModel Manufacturer { get; set; }
        public SellerModel Seller { get; set; }

        // Constructors
        public ToolModel()
        {
        }
        public ToolModel(string name)
        {
            Name = name;
            Serial = new SerialModel();
        }

        public ToolModel(string name, ManufacturerModel manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
        }

        public ToolModel(string name, ManufacturerModel manufacturer, SellerModel seller)
        {
            Name = name;
            Manufacturer = manufacturer;
            Seller = seller;
        }
    }
}
