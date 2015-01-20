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
        Driver m_driver;
        private LapDataProcessor m_lapDataProcessor;

        public MainWindow()
        {
            InitializeComponent();
            DecisionSequenceGenerator dsg = new DecisionSequenceGenerator(20);
            m_driver = new Driver("Robert Kubica", "BMW", 0.9, 0.4);
            m_lapDataProcessor = new LapDataProcessor(m_driver);
            this.laps.ItemsSource = m_lapDataProcessor.laps;
            this.wholeLapTime.Text = m_lapDataProcessor.GenerateLapTimeSum().ToString();

            StrategyOptimizer so = new StrategyOptimizer(m_lapDataProcessor, dsg);
            List<bool> best = so.GetBestStrategy();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool decision = false;
            if (this.chkDecision.IsChecked != null) ;
            {
                decision = (bool)this.chkDecision.IsChecked;
            }
            m_lapDataProcessor.SimulateLap(decision);
            this.laps.Items.Refresh();
            this.wholeLapTime.Text = m_lapDataProcessor.GenerateLapTimeSum().ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            m_lapDataProcessor.laps.Clear();
            m_lapDataProcessor.laps.Add(new LapData(100, 0, false, 0));
            this.laps.Items.Refresh();
            this.wholeLapTime.Text = m_lapDataProcessor.GenerateLapTimeSum().ToString();

        }
    }
}
