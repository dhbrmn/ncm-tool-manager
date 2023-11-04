using NcmToolManager.Library.Models;
using NcmToolManager.Library.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.UI.ViewModels
{
    public class PredogledViewModel : ViewModelBase
    {
        private List<IssuedToolModel> issuedTools;

        public List<IssuedToolModel> IssuedTools
        {
            get
            {
                return issuedTools;
            }
            set
            {
                issuedTools = value;
                OnPropertyChanged(nameof(IssuedTools));
            }
        }

        public PredogledViewModel()
        {
            LoadIssuedTools();
        }

        private void LoadIssuedTools()
        {
            IssuedTools = SqlServerAccess.GetIssuedToolsQuantity();
        }
    }
}
