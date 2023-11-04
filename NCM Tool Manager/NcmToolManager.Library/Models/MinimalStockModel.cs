using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class MinimalStockModel
    {
        public int Id {get; set; }
        public int ToolId {get; set; }
        public int MinimalStock {get; set; }

        public MinimalStockModel() { }
        public MinimalStockModel( int toolId, int minimalStock )
        {
            ToolId = toolId;
            MinimalStock = minimalStock;
        }

    }
}
