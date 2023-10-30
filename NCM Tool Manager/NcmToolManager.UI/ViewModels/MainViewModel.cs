using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NcmToolManager.Library.Models;
using NcmToolManager.Library.DataAccess;
using NcmToolManager.Library.Functions;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using FontAwesome.Sharp;
using System.Windows.Input;

namespace NcmToolManager.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _user;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        public UserModel User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //Commands
        public ICommand ShowPredogledViewCommand
        {
            get;
        }


        public MainViewModel()
        {
            User = new UserModel();

            //Initialize commands
            ShowPredogledViewCommand = new RelayCommand(ExecuteShowPredogledViewCommand);

            //Default view
            ExecuteShowPredogledViewCommand(null);

            //LoadCurrentUserData();
        }



        private void ExecuteShowPredogledViewCommand( object obj )
        {
            CurrentChildView= new PredogledViewModel();
            Caption = "Predogled";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            var user = SqlServerAccess.ReadUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                User.Name = user.Name;
                User.DisplayName = $"Welcome {user.Name} {user.LastName}";
            }
            else
            {
                User.DisplayName = "Uporabnik ni vpisan.";
                //Hide child views.
            }
        }
    }
}
