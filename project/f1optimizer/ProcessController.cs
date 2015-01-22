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
        private List<string> m_strategiesStrings;
        private Random m_random;

        #endregion Fields

        #region Constructor

        public ProcessController()
        {
            m_random = new Random();
            m_configReader = new ConfigReader("f1");
            m_configReader.GetGlobalConfig();
            drivers = m_configReader.GetDrivers();

            m_strategiesStrings = m_configReader.GetStrategies();
            m_strategiesStrings.Add("optimal");
            m_optimizer = new CompleteOverviewOptimizer(m_configReader.GetInt("lap_number"));
            GenerateLapProcessorsUsingDrivers();

        }

        #endregion

        #region Public Methods

        public LapDataProcessor ApplyStrategy(string strategy, LapDataProcessor driverProcessor)
        {
            switch (strategy.Substring(Math.Min(1, strategy.Length)))
            {
                case "o":
                    return PerformOptimization(driverProcessor);
                case "r":
                    return PerformRandomStrategy(strategy, driverProcessor);
                case "0":
                case "1":
                    return PerformFixedStrategy(strategy, driverProcessor);
                default:
                    return PerformOptimization(driverProcessor);
            }
        }

        #endregion

        #region Private Methods

        private void GenerateLapProcessorsUsingDrivers()
        {
            this.lapProcessors = new List<LapDataProcessor>();

            foreach (Driver driver in drivers)
            {
                LapDataProcessor ldp = new LapDataProcessor(driver)
                ldp.SetTimeMultiplier(m_configReader.GetDouble("time_multiplier");
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

        private LapDataProcessor PerformFixedStrategy(string sequence, LapDataProcessor driverProcessor)
        {
            driverProcessor.Clear();
            string resultSequence = "";
            int lapNumber = m_configReader.GetInt("lap_number");            
            
            while (resultSequence.Length < lapNumber)
            {
                StringBuilder sb = new StringBuilder(resultSequence);
                sb.Append(sequence);
                resultSequence = sb.ToString();
            }

            resultSequence = resultSequence.Substring(0, lapNumber);

            for (int i = 0; i < lapNumber; ++i)
            {
                driverProcessor.SimulateLap(resultSequence.ElementAt(i) == '1');
            }

            return driverProcessor;
        }

        private LapDataProcessor PerformRandomStrategy(string probability, LapDataProcessor driverProcessor)
        {
            driverProcessor.Clear();
            int percentage = Convert.ToInt32(probability.Substring(probability.Length - 2));
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
