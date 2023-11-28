using NcmToolManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NcmToolManager.UI.ViewModels;
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
        public DelavciView()
        {
            InitializeComponent();
        }

        private void izdanaOrodjaText_IsVisibleChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            if (this.allWorkersList.SelectedItem != null)
            {
                this.izdanaOrodjaText.Visibility = Visibility.Visible;
            }
            else
            {
                this.izdanaOrodjaText.Visibility = Visibility.Hidden;
            }
        }
    }
}
