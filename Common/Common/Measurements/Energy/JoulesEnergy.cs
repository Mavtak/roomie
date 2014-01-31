
namespace Roomie.Common.Measurements.Energy
{
    public class JoulesEnergy : IEnergy
    {
        private readonly double _value;

        public JoulesEnergy(double value)
        {
            _value = value;
        }

        public JoulesEnergy Joules
        {
            get
            {
                return this;
            }
        }

        public KilowattHoursEnergy KilowattHours
        {
            get
            {
                var result = new KilowattHoursEnergy(_value/3600000);

                return result;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public string Units
        {
            get
            {
                return "Joules";
            }
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
