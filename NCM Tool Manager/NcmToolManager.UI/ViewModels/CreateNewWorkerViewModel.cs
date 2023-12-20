using NcmToolManager.Library.DataAccess;
using NcmToolManager.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NcmToolManager.UI.ViewModels
{
    public class CreateNewWorkerViewModel : ViewModelBase
    {
        private string _name;
        private string _lastName;
        private bool _validData;
        private bool _isViewVisible = true;
        private string _errorMessage = "Ta delavec že obstaja";

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand CreateWorkerCommand { get; }
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public CreateNewWorkerViewModel()
        {
            CreateWorkerCommand = new RelayCommand(ExecuteCreateWorkerCommand, CanExecuteCreateWorkerCommand);
        }

        private void ExecuteCreateWorkerCommand( object obj )
        {
            _name = _name.ToUpper();
            _lastName = _lastName.ToUpper();

            if (SqlServerAccess.UserExists(_name, _lastName))
            {
                MessageBox.Show("Ta delavec že obstaja.");
                return;
            }

            SqlServerAccess.NewUser(_name, _lastName);

            MessageBox.Show("Delavec je shranjen.");

            IsViewVisible = false;
        }

        private bool CanExecuteCreateWorkerCommand( object obj )
        {
            _validData = true;
            
            if (string.IsNullOrWhiteSpace( Name ) || Name.Length <= 1 || Name.Any(char.IsDigit) )
            {
                _validData = false;
            }

            if (string.IsNullOrWhiteSpace(LastName) || LastName.Length <= 1 || LastName.Any(char.IsDigit) )
            {
                _validData = false;
            }

            return _validData;
        }
    }
}
