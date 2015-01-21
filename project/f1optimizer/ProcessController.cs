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

        #endregion Fields

        #region Constructor

        public ProcessController()
        {
            m_configReader = new ConfigReader("f1");
            m_configParams = m_configReader.GetGlobalConfig();
            drivers = m_configReader.GetDrivers();
            List<string> strs = m_configReader.GetStrategies();
        }

        #endregion


    }
}
