﻿
namespace Roomie.Common.HomeAutomation.Events
{
    public class DevicePowerChanged : DeviceStateChanged
    {
        public const string Key = "Device Power Changed";

        public virtual string Name
        {
            get
            {
                return Key;
            }
        }
    }
}
