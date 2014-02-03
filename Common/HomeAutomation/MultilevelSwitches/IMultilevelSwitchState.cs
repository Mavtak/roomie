﻿
namespace Roomie.Common.HomeAutomation.MultilevelSwitches
{
    public interface IMultilevelSwitchState
    {
        int? Power { get;}
        int? MaxPower { get; }

        void Update(IMultilevelSwitchState state);
    }
}
