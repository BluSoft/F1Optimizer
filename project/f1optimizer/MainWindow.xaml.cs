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
        private ObservableCollection<LapData> raceData1;
        private ObservableCollection<LapData> raceData2;

        public MainWindow()
        {
            InitializeComponent();
            m_processController = ProcessControllerFactory.GetProcessController();
            m_processController.GenerateSelectedLapData();
            m_model = m_processController.GetModel();
            raceData1 = new ObservableCollection<LapData>(m_model.LapsData[0].laps);
            raceData2 = new ObservableCollection<LapData>(m_model.LapsData[1].laps);
            DataContext = new
            {
                race1 = raceData1,
                race2 = this.raceData2

            };
        }


    }
}
