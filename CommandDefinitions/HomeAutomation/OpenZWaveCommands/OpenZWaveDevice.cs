using System.Collections.Generic;
using System.Linq;
using OpenZWaveDotNet;
using Roomie.CommandDefinitions.HomeAutomationCommands;
using Roomie.Common.HomeAutomation.DimmerSwitches;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Thermostats;
using Roomie.Common.HomeAutomation.ToggleSwitches;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class OpenZWaveDevice : Device
    {
        internal List<ZWValueID> Values { get; private set; }
        internal ZWManager Manager { get; private set; }
        internal byte Id { get; private set; }

        private readonly OpenZWaveToggleSwitch _toggleSwitch;
        private readonly OpenZWaveDimmerSwitch _dimmerSwitch;

        public OpenZWaveDevice(Network network, ZWManager manager, byte id)
            : base(network)
        {
            Manager = manager;
            Id = id;
            Values = new List<ZWValueID>();

            Address = Id.ToString();
            IsConnected = true;

            _toggleSwitch = new OpenZWaveToggleSwitch(this);
            _dimmerSwitch = new OpenZWaveDimmerSwitch(this);
        }

        internal ZWValueID GetValueByClassId(byte classId)
        {
            var result = Values.FirstOrDefault(x => x.GetCommandClassId() == classId);

            return result;
        }

        internal ZWValueID GetValueByClassId(CommandClass classId)
        {
            var result = GetValueByClassId((byte) classId);

            return result;
        }

        internal bool ProcessValueChanged(ZWValueID value)
        {
            if (_toggleSwitch.ProcessValueChanged(value))
            {
                return true;
            }

            if (_dimmerSwitch.ProcessValueChanged(value))
            {
                return true;
            }

            return false;
        }

        public override IToggleSwitch ToggleSwitch
        {
            get
            {
                return _toggleSwitch;
            }
        }

        public override IDimmerSwitch DimmerSwitch
        {
            get
            {
                return _dimmerSwitch;
            }
        }

        public override IThermostat Thermostat
        {
            get
            {
                return null;
            }
        }

        public override IKeypad Keypad
        {
            get
            {
                return null;
            }
        }

        public override string ToString()
        {
            return this.FormatData();
        }
    }
}
