using System.Xml.Linq;

namespace Roomie.Common.HomeAutomation.MultilevelSwitches
{
    public class ReadOnlyMultilevelSwitchState : IMultilevelSwitchState
    {
        public int? Power { get; private set; }
        public int? MaxPower { get; private set; }

        private ReadOnlyMultilevelSwitchState()
        {
        }

        public ReadOnlyMultilevelSwitchState(int? power, int? maxPower)
        {
            Power = power;
            MaxPower = maxPower;
        }

        public static ReadOnlyMultilevelSwitchState CopyFrom(IMultilevelSwitchState source)
        {
            var result = new ReadOnlyMultilevelSwitchState
            {
                Power = source.Power,
                MaxPower =  source.MaxPower
            };

            return result;
        }

        public void Update(IMultilevelSwitchState state)
        {
            throw new System.NotImplementedException();
        }

        public static ReadOnlyMultilevelSwitchState FromXElement(XElement element)
        {
            var power = element.GetAttributeIntValue("Power");
            var maxPower = element.GetAttributeIntValue("MaxPower");

            var result = new ReadOnlyMultilevelSwitchState
                {
                    Power = power,
                    MaxPower = maxPower
                };

            return result;
        }
    }
}
