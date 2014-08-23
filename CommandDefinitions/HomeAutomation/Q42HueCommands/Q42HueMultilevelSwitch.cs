using Roomie.Common.HomeAutomation.MultilevelSwitches;

namespace Q42HueCommands
{
    public class Q42HueMultilevelSwitch : IMultilevelSwitch
    {
        private readonly Q42HueDevice _light;

        public Q42HueMultilevelSwitch(Q42HueDevice light)
        {
            _light = light;
        }

        #region IMultilevelSwitch implementation

        public int? Power
        {
            get
            {
                return Helpers.CalculatePower(_light.Light);
            }
        }

        public int? MaxPower
        {
            get
            {
                return Helpers.MaxPower;
            }
        }

        public void Poll()
        {
            _light.UpdateState();
        }

        public void SetPower(int power)
        {
            var command = Helpers.CreateCommand(power);

            _light.SendCommand(command);
        }

        #endregion
    }
}
