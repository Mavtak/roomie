using Roomie.Common.Color;

namespace Roomie.Common.HomeAutomation.ColorSwitch
{
    public interface IColorSwitchActions
    {
        void SetValue(IColor color);
        void Poll();
    }
}
