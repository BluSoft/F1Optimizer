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

        private Dictionary<string, string> m_configParams;
        private ConfigReader m_configReader;
        private CompleteOverviewOptimizer m_optimizer;
        private List<string> m_strategiesStrings;

        #endregion Fields

        #region Constructor

        public ProcessController()
        {
            m_configReader = new ConfigReader("f1");
            m_configParams = m_configReader.GetGlobalConfig();
            drivers = m_configReader.GetDrivers();
            m_strategiesStrings = m_configReader.GetStrategies();
            GenerateLapProcessorsUsingDrivers();

        }

        #endregion

        #region Private Methods

        private void GenerateLapProcessorsUsingDrivers()
        {
            List<LapDataProcessor> lapProcessors = new List<LapDataProcessor>();

            foreach (Driver driver in drivers)
            {
                lapProcessors.Add(new LapDataProcessor(driver));
            }
        }

        #endregion
    }
}
