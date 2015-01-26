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

        public MainModel(List<LapDataProcessor> list, List<Driver> drivers, List<String> strategies)
        {
            m_lapsData = list;
            m_drivers = drivers;
            m_strategiesStrings = strategies;
        }

        #endregion

        #region Fields

        private List<LapDataProcessor> m_lapsData;
        private List<Driver> m_drivers;
        List<string> m_strategiesStrings;

        #endregion Fields
    }
}
