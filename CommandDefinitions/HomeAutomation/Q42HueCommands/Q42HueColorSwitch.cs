using System;
using Q42.HueApi;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation.ColorSwitch;

namespace Q42HueCommands
{
    public class Q42HueColorSwitch : IColorSwitch
    {
        private readonly Q42HueDevice _light;

        public Q42HueColorSwitch(Q42HueDevice light)
        {
            _light = light;
        }

        #region IColorSwitch implementation

        public IColor Value
        {
            get
            {
                var result = Helpers.CalculateColor(_light.BackingObject);

                return result;
            }
        }

        public void SetValue(IColor color)
        {
            var command = Helpers.CreateCommand(color);

            _light.SendCommand(command);
        }

        public void Poll()
        {
            _light.UpdateState();
        }

        #endregion
    }
}
