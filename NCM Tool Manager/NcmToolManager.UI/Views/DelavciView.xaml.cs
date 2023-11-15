using NcmToolManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NcmToolManager.UI.Views
{
    /// <summary>
    /// Interaction logic for DelavciView.xaml
    /// </summary>
    public partial class DelavciView : UserControl
    {
        private string _workerName;
        public DelavciView()
        {
            InitializeComponent();
        }

        public string WorkerName
        {
            get => _workerName;
            set => _workerName =  value ;
        }

        private void allWorkersList_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            UserModel selectedUser = (UserModel)this.allWorkersList.SelectedItem;
            WorkerName = selectedUser.Name + " " + selectedUser.LastName;
            this.workerName.Text = WorkerName;
        }
    }
}
