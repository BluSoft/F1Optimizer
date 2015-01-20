using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class DecisionSequenceGenerator
    {
        #region Fields

        private int m_lapNumber;

        private int m_combinationsNumber;

        #endregion

        #region Constructors

        public DecisionSequenceGenerator(int lapNumber)
        {
            m_lapNumber = lapNumber;
            m_combinationsNumber = Convert.ToInt32(Math.Pow(2.0, Convert.ToDouble(m_lapNumber)));
        }

        #endregion

        #region Public Methods

        public List<List<bool>> GenerateAllDecisions()
        {
            List<String> results = new List<String>();
            List<List<bool>> output = new List<List<bool>>();
            
            for (int i = 0; i < m_combinationsNumber; ++i)
            {
                String placeholder = new String('0', m_lapNumber);
                String b = Convert.ToString(i, 2);

                StringBuilder sb = new StringBuilder(placeholder);
                sb.Remove(placeholder.Length - b.Length, b.Length);
                sb.Insert(placeholder.Length - b.Length, b);

                results.Add(sb.ToString());
            }

            foreach (var row in results)
            {
                List<bool> lsit = row.Select(chr => chr == '1').ToList();
                output.Add(row.Select(chr => chr == '1').ToList());
            }

            return output;
        }

        #endregion Public Methods
    }
}
