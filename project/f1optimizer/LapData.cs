using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class LapData
    {

        #region Fields

        public int LapNumber { get; set; }
        public double TyreWear { get; set; }
        public double LapTime { get; set; }
        public bool Decision { get; set; }
        
        #endregion

        #region Constructor

        public LapData(double tyreWear, double lapTime, bool decision, int lapNumber)
        {
            this.TyreWear = tyreWear;
            this.LapTime = lapTime;
            this.Decision = decision;
            this.LapNumber = lapNumber;
        }

        #endregion constructor
    }
}
