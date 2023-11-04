using NcmToolManager.Library.Models;
using NcmToolManager.Library.DataAccess;
using System.Threading;
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

            LoadCurrentUserData();
        }



        private void ExecuteShowPredogledViewCommand( object obj )
        {
            CurrentChildView= new PredogledViewModel();
            Caption = "Predogled";
            Icon = IconChar.Home;
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
            }
            else
            {
                User.DisplayName = "Uporabnik ni vpisan";
                //Hide child views.
            }
        }
    }
}
