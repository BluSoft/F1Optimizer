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

namespace f1optimizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProcessController m_processController;
        private MainModel m_model;

        public MainWindow()
        {
            InitializeComponent();
            m_processController = new ProcessController();
            m_model = m_processController.GetModel();



            CompleteOverviewOptimizer so = new CompleteOverviewOptimizer(20);
            List<bool> best = so.GetBestStrategy();
        }


    }
}
