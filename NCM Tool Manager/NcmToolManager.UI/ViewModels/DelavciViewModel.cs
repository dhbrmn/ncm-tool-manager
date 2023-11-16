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
        private string workerName;
        private UserModel selectedWorker;
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

        public string WorkerName
        {
            get
            {
                return workerName;
            }
            set
            {
                workerName = value;
                OnPropertyChanged(nameof(WorkerName));
            }
        }

        public UserModel SelectedWorker
        {
            get
            {
                return selectedWorker;
            }
            set
            {
                selectedWorker = value;
                OnPropertyChanged(nameof(SelectedWorker));
                WorkerName = SelectedWorker.Name + " " + SelectedWorker.LastName;
            }
        }

        private void LoadAllWorkers()
        {
            AllWorkers = SqlServerAccess.GetAllUsers();
        }
    }
}
