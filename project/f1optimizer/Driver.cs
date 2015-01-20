using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f1optimizer
{
    class Driver
    {
        #region Fields

        public String Name { get; set; }

        public String Team { get; set; }

        public double NormalLapTime { get {
            return LAPTIME_BASE * (2.0 - SkillCoefficient);
        } }

        public double TyreWearCoefficient { get {
            return TYREWEAR_BASE * AggressionCoefficient;
        } }

        public double SkillCoefficient { get; set; }

        public double AggressionCoefficient { get; set; }

        private const double LAPTIME_BASE = 110.0;

        private const double TYREWEAR_BASE = 0.5;

        public double timeMultiplier = 3;

        #endregion Fields

        #region Constructor

        public Driver(String name, String team, double skillCoefficient, double aggressionCoefficient)
        {
            Name = name;
            Team = team;
            SkillCoefficient = skillCoefficient;
            AggressionCoefficient = aggressionCoefficient;
        }

        #endregion
    }
}
