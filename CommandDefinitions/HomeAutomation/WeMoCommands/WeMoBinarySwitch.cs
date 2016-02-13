using System;
using Roomie.Common.HomeAutomation.BinarySwitches;
using Roomie.CommandDefinitions.WeMoCommands.Sklose;

namespace Roomie.CommandDefinitions.WeMoCommands
{
    public class WeMoBinarySwitch : IBinarySwitch
    {
        private BasicServicePortTypeClient _client;
        private BinarySwitchPower? _power;

        public WeMoBinarySwitch(BasicServicePortTypeClient client)
        {
            _client = client;
        }

        public BinarySwitchPower? Power
        {
            get
            {
                return _power;
            }
        }

        public void Poll()
        {
            var binaryState = _client.GetBinaryState(new GetBinaryState()).BinaryState;

            _power = TranslateBinaryStateToPower(binaryState);
        }

        public void SetPower(BinarySwitchPower power)
        {
            var binaryState = TranslatePowerToBinaryState(power);

            _client.SetBinaryState(new SetBinaryState
            {
                BinaryState = binaryState
            });
        }

        private static BinarySwitchPower? TranslateBinaryStateToPower(string binaryState)
        {
            switch (binaryState)
            {
                case "0":
                    return BinarySwitchPower.Off;

                case "1":
                    return BinarySwitchPower.On;

                default:
                    return null;
            }
        }

        private static string TranslatePowerToBinaryState(BinarySwitchPower power)
        {
            switch (power)
            {
                case BinarySwitchPower.Off:
                    return "0";

                case BinarySwitchPower.On:
                    return "1";

                default:
                    throw new ArgumentException("invalid BinarySwitchPower value \"" + power + "\"", "power");
            }
        }
    }
}
