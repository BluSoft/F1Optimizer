using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class Bolid
    {
        #region Constants

        public double NormalLapTime {get;set;}
        
        #endregion Constants

        #region Constructors

        public Bolid(double lapTime)
        {
            this.NormalLapTime = lapTime;
        }

        #endregion Constructors
    }
}
