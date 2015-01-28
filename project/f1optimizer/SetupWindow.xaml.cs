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
        private ObservableCollection<Driver> driverList;
        private MainModel model;
        private ProcessController processController;

        public SetupWindow()
        {
            processController = ProcessControllerFactory.GetProcessController();
            model = processController.GetModel();
            driverList = model.Drivers;
            DataContext = driverList;
            InitializeComponent();
        }
    }
}
