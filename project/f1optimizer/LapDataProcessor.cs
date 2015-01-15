using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class LapDataProcessor
    {
        #region Fields

        public List<LapData> laps;
        public Bolid drivingBolid;

        #endregion Fields

        #region Constants

        private const double TIME_MULTIPLIER = 3;
        private const double TYRE_WEAR_MULTIPLIER = 0.1;

        #endregion Constants

        #region Constructor

        public LapDataProcessor(Bolid bolid)
        {
            this.drivingBolid = bolid;
            laps = new List<LapData>();
            laps.Add(new LapData(100, 0, false, 0));
        }

        #endregion Constructor

        #region Public Methods

        public void SimulateLap(bool decision)
        {
            laps.Add(this.DriveLap(laps.Last().TyreWear, this.drivingBolid.NormalLapTime, decision, laps.Count));        
        }

        public double GenerateLapTimeSum()
        {
            double sum = 0;
            foreach (var lap in laps)
            {
                sum += lap.LapTime;
            }
            return sum;
        }

        #endregion Public Methods


        #region Private Methods

        private LapData DriveLap(double currentTyreWear, double bolidNormalLapTime, bool decision, int lapNumber)
        {
            double newTyreWear = 0;
            double lapTime = 0;
            double lapTimeChange = GenerateTimeChange(currentTyreWear);
            if (decision)
            {
                newTyreWear = 100;
                lapTime = bolidNormalLapTime + lapTimeChange + 20;
            }
            else
            {
                double tyreWearOnLap = GenerateTyreWearOnLap(currentTyreWear);
                newTyreWear = currentTyreWear - tyreWearOnLap;
                lapTime = bolidNormalLapTime + lapTimeChange;
            }
           
            return new LapData(newTyreWear, lapTime, decision, lapNumber);
        }

        private double GenerateTyreWearOnLap(double currentTyreWear)
        {
            return TYRE_WEAR_MULTIPLIER * (100 - currentTyreWear) + 1;
        }

        private double GenerateTimeChange(double currentTyreWear)
        {
            return TIME_MULTIPLIER * (100 - currentTyreWear) + 0.2;
        }

        #endregion Private Methods

    }
}
