using NcmToolManager.Library.Models;
using NcmToolManager.Library.DataAccess;
using System.Threading;
using FontAwesome.Sharp;
using System.Windows.Input;
using System.Windows.Forms.VisualStyles;

namespace NcmToolManager.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _user;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private bool _isSignedIn;

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
        public bool IsSignedIn
        {
            get => _isSignedIn;
            set
            {
                _isSignedIn = value;
                OnPropertyChanged(nameof(IsSignedIn));
            }
        }


        //Commands
        public ICommand ShowPredogledViewCommand
        {
            get;
        }
        public ICommand ShowDelavciViewCommand
        {
            get;
        }
        public ICommand ShowOrodjaViewCommand
        {
            get;
        }
        public ICommand ShowDobaviteljiViewCommand
        {
            get;
        }
        public ICommand ShowIzdajaViewCommand
        {
            get;
        }




        public MainViewModel()
        {
            User = new UserModel();

            //Initialize commands
            ShowPredogledViewCommand = new RelayCommand(ExecuteShowPredogledViewCommand);
            ShowDelavciViewCommand = new RelayCommand(ExecuteShowDelavciViewCommand);
            ShowOrodjaViewCommand = new RelayCommand(ExecuteShowOrodjaViewCommand);
            ShowDobaviteljiViewCommand = new RelayCommand(ExecuteShowDobaviteljiViewCommand);
            ShowIzdajaViewCommand = new RelayCommand(ExecuteShowIzdajaViewCommand);

            //Default view
            ExecuteShowPredogledViewCommand(null);

            LoadCurrentUserData();
        }



        private void ExecuteShowPredogledViewCommand( object? obj )
        {
            CurrentChildView= new PredogledViewModel();
            Caption = "Predogled";
            Icon = IconChar.Home;
        }

        private void ExecuteShowDelavciViewCommand( object? obj )
        {
            CurrentChildView = new DelavciViewModel();
            Caption = "Delavci";
            Icon = IconChar.HelmetSafety;
        }
        private void ExecuteShowOrodjaViewCommand( object? obj )
        {
            CurrentChildView = new OrodjaViewModel();
            Caption = "Orodja";
            Icon = IconChar.Toolbox;
        }
        private void ExecuteShowDobaviteljiViewCommand( object? obj )
        {
            CurrentChildView = new DobaviteljiViewModel();
            Caption = "Dobavitelji";
            Icon = IconChar.PersonWalkingLuggage;
        }
        private void ExecuteShowIzdajaViewCommand( object? obj )
        {
            CurrentChildView = new IzdajaViewModel();
            Caption = "Izdaja in prejem orodja";
            Icon = IconChar.Tools;
        }




        private void LoadCurrentUserData()
        {
            UserModel user = new();

            if (Thread.CurrentPrincipal != null)
            {
                user = SqlServerAccess.ReadUserByUsername(Thread.CurrentPrincipal.Identity.Name);
            }

            if (user.Name != null && user.Name != "")
            {
                User.Name = user.Name;
                User.DisplayName = $"Dobrodošel {user.Name} {user.LastName}";
                IsSignedIn = true;
            }
            else
            {
                User.DisplayName = "Uporabnik ni vpisan";
                IsSignedIn = false;
            }
        }

    }
}
