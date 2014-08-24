using System;
using Q42.HueApi;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.MultilevelSwitches;

namespace Q42HueCommands
{
    public class Q42HueDevice : Device
    {
        internal readonly Q42HueNetwork HueNetwork;
        public Light Light { get; private set; }

        private readonly Q42HueBinarySwitch _binarySwitch;
        private readonly Q42HueMultilevelSwitch _multilevelSwitch;
        private readonly Q42HueColorSwitch _colorSwitch;

        public Q42HueDevice(Q42HueNetwork network, Light light)
            : base(network, DeviceType.MultilevelSwitch)
        {
            HueNetwork = network;
            Light = light;

            _binarySwitch = new Q42HueBinarySwitch(this);
            _multilevelSwitch = new Q42HueMultilevelSwitch(this);
            _colorSwitch = new Q42HueColorSwitch(this);
            
            Name = light.Name;
            Address = light.Id;
            IsConnected = light.State.IsReachable;
        }

        internal void SendCommand(LightCommand command)
        {
            HueNetwork.SendCommand(command, this);
            HueNetwork.UpdateDevice(this);
        }

        internal void UpdateState()
        {
            HueNetwork.UpdateDevice(this);
        }

        internal void UpdateLight(Light newLight)
        {
            if (Light.Id != newLight.Id)
            {
                throw new Exception("Light ID does not match");
            }

            var oldLight = Light;

            Light = newLight;

            if (oldLight.State.IsReachable != newLight.State.IsReachable)
            {
                AddEvent(newLight.State.IsReachable ? DeviceEvent.Found(this, null) : DeviceEvent.Lost(this, null));
            }

            if (Helpers.CalculatePower(oldLight) != Helpers.CalculatePower(newLight))
            {
                AddEvent(DeviceEvent.PowerChanged(this, null));
            }

            var oldColor = Helpers.CalculateColor(oldLight);
            var newColor = Helpers.CalculateColor(newLight);

            if (!oldColor.RedGreenBlue.Equals(newColor.RedGreenBlue))
            {
                AddEvent(DeviceEvent.ColorChanged(this, null));
            }
        }

        public override IBinarySwitch BinarySwitch
        {
            get
            {
                return _binarySwitch;
            }
        }

        public override IMultilevelSwitch MultilevelSwitch
        {
            get
            {
                return _multilevelSwitch;
            }
        }

        public override Roomie.Common.HomeAutomation.ColorSwitch.IColorSwitch ColorSwitch
        {
            get
            {
                return _colorSwitch;
            }
        }
    }
}
