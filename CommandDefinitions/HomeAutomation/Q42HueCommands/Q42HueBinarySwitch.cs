using Roomie.Common.HomeAutomation.BinarySwitches;

namespace Q42HueCommands
{
    public class Q42HueBinarySwitch : IBinarySwitch
    {
        private readonly Q42HueDevice _light;

        public Q42HueBinarySwitch(Q42HueDevice light)
        {
            _light = light;
        }
        
        #region IBinarySwitch implementation

        public BinarySwitchPower? Power
        {
            get
            {
                return null;
            }
        }

        public void SetPower(BinarySwitchPower power)
        {
            var command = Helpers.CreateCommand(power);

            _light.SendCommand(command);
        }

        public void Poll()
        {
           _light.Poll();
        }

        #endregion
    }
}
