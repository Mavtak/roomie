
namespace Roomie.Common.Measurements.Energy
{
    public class KilowattHoursEnergy : IEnergy
    {
        private readonly double _value;

        public KilowattHoursEnergy(double value)
        {
            _value = value;
        }

        public JoulesEnergy Joules
        {
            get
            {
                var result = new JoulesEnergy(_value*3600000);

                return result;
            }
        }

        public KilowattHoursEnergy KilowattHours
        {
            get
            {
                return this;
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
                return "Kilowatt Hours";
            }
        }

        public override string ToString()
        {
            return this.Format();
        }
    }
}
