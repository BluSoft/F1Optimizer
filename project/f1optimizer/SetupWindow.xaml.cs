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
        private ObservableCollection<Strategy> strategyList;
        private MainModel model;
        private ProcessController processController;

        private Driver m_selectedDriver1;
        private Driver m_selectedDriver2;

        private Strategy m_selectedStrategy1;
        private Strategy m_selectedStrategy2;

        public SetupWindow()
        {
            processController = ProcessControllerFactory.GetProcessController();
            model = processController.GetModel();
            driverList = new ObservableCollection<Driver>(model.Drivers);
            strategyList = new ObservableCollection<Strategy>(model.Strategies);
            DataContext = new 
            {
                drivers = driverList,
                strategies = strategyList

            };
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Strategie1.SelectionChanged += Strategie1_SelectionChanged;
            this.Strategie2.SelectionChanged += Strategie2_SelectionChanged;
            this.Kierowcy1.SelectionChanged += Kierowcy1_SelectionChanged;
            this.Kierowcy2.SelectionChanged += Kierowcy2_SelectionChanged;
            this.startRace.Click += startRace_Click;
        }

        void startRace_Click(object sender, RoutedEventArgs e)
        {
            List<Driver> drivers = new List<Driver>();
            drivers.Add(m_selectedDriver1);
            drivers.Add(m_selectedDriver2);

            List<Strategy> strategies = new List<Strategy>();
            m_selectedStrategy1.Option = Convert.ToInt32(this.Parametr1.Value);
            m_selectedStrategy2.Option = Convert.ToInt32(this.Parametr2.Value);
            strategies.Add(m_selectedStrategy1);
            strategies.Add(m_selectedStrategy2);

            this.model.SelectedDrivers = drivers;
            this.model.SelectedStrategies = strategies;

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        void Kierowcy2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Driver dr = (Driver)this.Kierowcy1.SelectedItem;
            this.Name2.Content = dr.Name;
            this.Team2.Content = dr.Team;
            this.Speed2.Content = dr.SkillCoefficient.ToString();
            this.Aggr2.Content = dr.AggressionCoefficient.ToString();
            m_selectedDriver2 = dr;
        }

        void Kierowcy1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Driver dr = (Driver)this.Kierowcy1.SelectedItem;
            this.Name1.Content = dr.Name;
            this.Team1.Content = dr.Team;
            this.Speed1.Content = dr.SkillCoefficient.ToString();
            this.Aggr1.Content = dr.AggressionCoefficient.ToString();
            m_selectedDriver1 = dr;
        }

        void Strategie2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Strategy str = (Strategy)this.Strategie2.SelectedItem;
            this.Description2.Text = str.Description;
            m_selectedStrategy2 = str;
        }

        void Strategie1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Strategy str = (Strategy)this.Strategie1.SelectedItem;
            this.Description1.Text = str.Description;
            m_selectedStrategy1 = str;
        }

        private void Parametr2_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ParamValue2.Content = e.NewValue.ToString();
        }

        private void Parametr1_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ParamValue1.Content = e.NewValue.ToString();
        }


    }
}
