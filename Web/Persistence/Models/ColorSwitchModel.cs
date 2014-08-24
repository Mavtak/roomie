using System;
using Roomie.Common.Color;
using Roomie.Common.HomeAutomation.ColorSwitch;

namespace Roomie.Web.Persistence.Models
{
    public class ColorSwitchModel : IColorSwitch
    {
        private readonly DeviceModel _device;

        public ColorSwitchModel(DeviceModel deviceModel)
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
            throw new NotImplementedException();
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}