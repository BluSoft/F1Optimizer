using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class StrategyOptimizer
    {
        #region Fields

        private LapDataProcessor m_lapDataProcessor;
        private DecisionSequenceGenerator m_decisionGenerator;
        private List<double> m_timeList;
        
        #endregion Fields

        #region Constructors

        public StrategyOptimizer(LapDataProcessor ldp, DecisionSequenceGenerator dsg)
        {
            m_decisionGenerator = dsg;
            m_lapDataProcessor = ldp;
            m_timeList = new List<double>();
        }

        #endregion

        #region Public Methods

        public List<bool> GetBestStrategy()
        {
            List<bool> bestResult = new List<bool>();
            double bestTime = double.PositiveInfinity;

            foreach(var list in m_decisionGenerator.GenerateAllDecisions())
            {
                m_lapDataProcessor.laps.Clear();
                m_lapDataProcessor.laps.Add(new LapData(100, 0, false, 0));

                double sequenceTime = 0;
                foreach (var decision in list)
                {
                    m_lapDataProcessor.SimulateLap(decision);
                }
                sequenceTime = m_lapDataProcessor.GenerateLapTimeSum();
                m_timeList.Add(sequenceTime);
                if(sequenceTime < bestTime)
                {
                    bestTime = sequenceTime;
                    bestResult.Clear();
                    bestResult = list;
                }

            }

            return bestResult;
        }

        #endregion
    }
}
