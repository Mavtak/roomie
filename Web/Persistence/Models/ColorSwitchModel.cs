using System;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation.ColorSwitch;
using Roomie.Web.Persistence.Helpers;

namespace Roomie.Web.Persistence.Models
{
    public class ColorSwitchModel : IColorSwitch
    {
        private readonly Device _device;

        public ColorSwitchModel(Device deviceModel)
        {
            _device = deviceModel;
        }

        public void Update(IColorSwitchState state)
        {
            Value = state.Value;
        }

        #region IColorSwitch implementation

        public IColor Value { get; set; }

        public void SetValue(IColor color)
        {
            _device.DoCommand("SetColor", "Color", color.ToHexString());
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}