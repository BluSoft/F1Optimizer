using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class MainModel
    {
        #region Constructors

        public MainModel(List<Driver> drivers, List<Strategy> strategies)
        {
            m_drivers = drivers;
            m_strategies = strategies;
            m_lapsData = new List<LapDataProcessor>();
        }

        #endregion

        #region Fields

        private bool isRandom;

        public bool IsRandom
        {
            get { return isRandom; }
            set { isRandom = value; }
        }

        private List<LapDataProcessor> m_lapsData;

        public List<LapDataProcessor> LapsData
        {
            get { return m_lapsData; }
            set { m_lapsData = value; }
        }
        private List<Driver> m_drivers;
        private List<Strategy> m_strategies;


        private List<Driver> selectedDrivers;
        public List<Driver> SelectedDrivers
        {
            get { return selectedDrivers; }
            set { selectedDrivers = value; }
        }


        private List<Strategy> selectedStrategies;

        public List<Strategy> SelectedStrategies
        {
            get { return selectedStrategies; }
            set { selectedStrategies = value; }
        }

        public List<Driver> Drivers
        {
            get { return m_drivers; }
            set { m_drivers = value; }
        }

        internal List<Strategy> Strategies
        {
            get { return m_strategies; }
            set { m_strategies = value; }
        }

        #endregion Fields
    }
}
