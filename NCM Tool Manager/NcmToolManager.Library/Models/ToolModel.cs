using NcmToolManager.Library.Models;

namespace NcmToolManager.Library.Models
{
    public class ToolModel
    {
        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SerialId { get; set; }
        public int ManufacturerId { get; set; }

        // Constructors
        public ToolModel(string name)
        {
            Name = name;
        }
    }
}
