
using System.Xml.Linq;
namespace Roomie.Common.HomeAutomation.DimmerSwitches
{
    public class ReadOnlyDimmerSwitchState : IDimmerSwitchState
    {
        public int? Power { get; private set; }
        public int? MaxPower { get; private set; }

        private ReadOnlyDimmerSwitchState()
        {
        }

        public ReadOnlyDimmerSwitchState(int? power, int? maxPower)
        {
            Power = power;
            MaxPower = maxPower;
        }

        public static ReadOnlyDimmerSwitchState CopyFrom(IDimmerSwitchState source)
        {
            var result = new ReadOnlyDimmerSwitchState
            {
                Power = source.Power,
                MaxPower =  source.MaxPower
            };

            return result;
        }

        public void Update(IDimmerSwitchState state)
        {
            throw new System.NotImplementedException();
        }

        public static ReadOnlyDimmerSwitchState FromXElement(XElement element)
        {
            var power = element.GetAttributeIntValue("Power");
            var maxPower = element.GetAttributeIntValue("MaxPower");

            var result = new ReadOnlyDimmerSwitchState
                {
                    Power = power,
                    MaxPower = maxPower
                };

            return result;
        }
    }
}
