using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.UI.ViewModels
{
    public class DelavciViewModel : ViewModelBase
    {
        private List<UserModel> allWorkers;
        public DelavciViewModel()
        {
            LoadAllWorkers();
        }

        public List<UserModel> AllWorkers
        {
            get
            {
                return allWorkers;
            }
            set
            {
                allWorkers = value;
                OnPropertyChanged(nameof(AllWorkers));
            }
        }

        private void LoadAllWorkers()
        {
            AllWorkers = SqlServerAccess.GetAllUsers();
        }
    }
}
