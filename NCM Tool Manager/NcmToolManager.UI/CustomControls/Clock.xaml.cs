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

namespace NcmToolManager.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        System.Windows.Threading.DispatcherTimer Timer = new();
        public Clock()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }
        private void Timer_Tick( object sender, EventArgs e )
        {
            DateTime dateTime = DateTime.Now;
            timeString.Text = dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;
            dateString.Text = dateTime.Day + ". " + dateTime.Month + ". " + dateTime.Year;
        }

    }
}
