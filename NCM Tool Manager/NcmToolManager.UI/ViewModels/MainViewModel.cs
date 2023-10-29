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

namespace NcmToolManager.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _user;

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


        public MainViewModel()
        {
            User = new UserModel();
            LoadCurrentUserData();
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
