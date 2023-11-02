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
        private string _time;
        private string _date;


        public Clock()
        {
            InitializeComponent();
        }

        public string Time
        {
            get => _time;
            set
            {
                _time = TimeOnly.FromDateTime(DateTime.Now).ToString();
                _time = value;
            }
        }
        public string Date
        {
            get => _date;
            set
            {
                _date = DateOnly.FromDateTime(DateTime.Now).ToString();
                _date = value;
            }
        }
    }
}
