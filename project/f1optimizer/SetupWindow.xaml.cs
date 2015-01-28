using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace f1optimizer
{
    /// <summary>
    /// Interaction logic for SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        ObservableCollection<String> driverList;

        public SetupWindow()
        {
            driverList = new ObservableCollection<String>(new List<String>(){
        "Robert Kubica", "Lewis Hamilton"
        });
            DataContext = driverList;
            InitializeComponent();
        }
    }
}
