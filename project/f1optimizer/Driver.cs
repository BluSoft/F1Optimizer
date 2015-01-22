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
            return m_laptimeBase * (2.0 - SkillCoefficient);
        } }

        public double TyreWearCoefficient { get {
            return m_tyreWearBase * AggressionCoefficient;
        } }

        public double SkillCoefficient { get; set; }

        public double AggressionCoefficient { get; set; }

        private double m_laptimeBase = 110.0;

        private double m_tyreWearBase = 0.5;

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

        #region Public Methods

        public void SetTyreWearBase(double t)
        {
            m_tyreWearBase = t;
        }

        public void SetLapTimeBase(double t)
        {
            m_laptimeBase = t;
        }

        #endregion
    }
}
