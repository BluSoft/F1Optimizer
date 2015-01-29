using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class ProcessController
    {
        #region Fields

        public List<Driver> drivers;
        public List<LapDataProcessor> lapProcessors;

        private ConfigReader m_configReader;
        private CompleteOverviewOptimizer m_optimizer;
        private List<Strategy> m_strategies;
        private Random m_random;
        private MainModel m_model;

        #endregion Fields

        #region Constructor

        public ProcessController()
        {
            m_random = new Random();
            m_configReader = new ConfigReader("f1");
            m_configReader.GetGlobalConfig();
            drivers = m_configReader.GetDrivers();

            m_strategies = m_configReader.GetStrategies();
            m_optimizer = new CompleteOverviewOptimizer(m_configReader.GetInt("lap_number"));
            GenerateLapProcessorsUsingDrivers();

            m_model = new MainModel(drivers, m_strategies);
            m_model.IsRandom = m_configReader.GetBool("random");

        }

        #endregion

        #region Public Methods

        public MainModel GetModel()
        {
            return this.m_model;
        }

        public LapDataProcessor ApplyStrategy(Strategy strategy, LapDataProcessor driverProcessor)
        {
            switch (strategy.Name)
            {
                case "Optymalna":
                    return PerformOptimization(driverProcessor);
                case "Losowa":
                    return PerformRandomStrategy(strategy, driverProcessor);
                case "Ustalona":
                    return PerformFixedStrategy(strategy, driverProcessor);
                default:
                    return PerformOptimization(driverProcessor);
            }
        }

        #endregion

        #region Private Methods

        public void GenerateSelectedLapData()
        {
            var selectedDrivers = this.m_model.SelectedDrivers;
            var selectedStrategies = this.m_model.SelectedStrategies;
            var lapData = new List<LapDataProcessor>();

            for (int i = 0; i < selectedDrivers.Count; ++i)
            {
                LapDataProcessor ldp = new LapDataProcessor(selectedDrivers[i], this.m_random);
                ldp.SetTimeMultiplier(m_configReader.GetDouble("time_multiplier"));
                ldp.SetRandomTo(m_configReader.GetInt("randomTo"));
                ldp.SetRandomFrom(m_configReader.GetInt("randomFrom"));
                ldp = ApplyStrategy(selectedStrategies[i], ldp);
                lapData.Add(ldp);
            }
            this.m_model.LapsData = lapData;

        }

        private void GenerateLapProcessorsUsingDrivers()
        {
            this.lapProcessors = new List<LapDataProcessor>();

            foreach (Driver driver in drivers)
            {
                LapDataProcessor ldp = new LapDataProcessor(driver, this.m_random);
                ldp.SetTimeMultiplier(m_configReader.GetDouble("time_multiplier"));
                ldp.SetUseRandom(m_configReader.GetBool("random"));
                ldp.SetRandomTo(m_configReader.GetInt("randomTo"));
                ldp.SetRandomFrom(m_configReader.GetInt("randomFrom"));
                this.lapProcessors.Add(ldp);
            }
        }

        private LapDataProcessor PerformOptimization(LapDataProcessor driverProcessor)
        {
            driverProcessor.Clear();
            m_optimizer.SetLapDataProcessor(driverProcessor);
            m_optimizer.PerformCompleteOverviewOptimization();
            return driverProcessor;
        }

        private LapDataProcessor PerformFixedStrategy(Strategy strategy, LapDataProcessor driverProcessor)
        {
            driverProcessor.Clear();
            int lapNumber = m_configReader.GetInt("lap_number");            

            for (int i = 0; i < lapNumber; ++i)
            {
                bool test = (((i+1) % strategy.Option) == 0);
                driverProcessor.SimulateLap(test);
            }

            return driverProcessor;
        }

        private LapDataProcessor PerformRandomStrategy(Strategy strategy, LapDataProcessor driverProcessor)
        {
            driverProcessor.Clear();
            int percentage = Convert.ToInt32(strategy.Option);
            int lapNumber = m_configReader.GetInt("lap_number");

            for (int i = 0; i < lapNumber; ++i)
            {
                int rnd = m_random.Next(1, 100);
                if (rnd < percentage)
                {
                    driverProcessor.SimulateLap(true);
                }
                else 
                {
                    driverProcessor.SimulateLap(false);
                }
            }

            return driverProcessor;
        }

        private void UpdateDriversWithConfig()
        {            
            double lapTimeBase = m_configReader.GetDouble("laptime_base");
            double tyreWearBase = m_configReader.GetDouble("tyrewear_base");

            foreach (var driver in drivers)
            {
                driver.SetLapTimeBase(lapTimeBase);
                driver.SetTyreWearBase(tyreWearBase);
            }
        }

        #endregion
    }
}
