using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Models;
using NcmToolManager.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NcmToolManager.UI.ViewModels
{
    public class DelavciViewModel : ViewModelBase
    {
        private List<UserModel> allWorkers;
        private string workerName;
        private UserModel selectedWorker;
       

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

        public ICommand OpenCreateUserWindowCommand { get; }

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

        public DelavciViewModel()
        {
            LoadAllWorkers();
            OpenCreateUserWindowCommand = new RelayCommand(OpenCreateUserWindow, CanOpenCreateUserWindow);
        }

        private bool CanOpenCreateUserWindow( object obj )
        {
            return true;
        }

        private void OpenCreateUserWindow( object obj )
        {
            CreateNewWorker newWorkerWindow = new CreateNewWorker();
            newWorkerWindow.ShowDialog();
            LoadAllWorkers();
        }

        private void LoadAllWorkers()
        {
            AllWorkers = SqlServerAccess.GetAllUsers();
        }


    }
}
