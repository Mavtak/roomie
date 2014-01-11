using System.Collections.Generic;
using System.Linq;
using PIEHidDotNet;
using Roomie.Common.HomeAutomation.Events;
using Roomie.Common.HomeAutomation.Keypads;
using Roomie.Common.HomeAutomation.Keypads.Buttons;

namespace Roomie.CommandDefinitions.PiEngineeringCommands
{
    public class PiEngineeringKeypad : IKeypad, PIEDataHandler
    {
        public IEnumerable<IKeypadButtonState> Buttons { get; private set; }

        private PiEngineeringDevice _device;
        private LinkedList<IKeypadState> _history;

        public PiEngineeringKeypad(PiEngineeringDevice device)
        {
            _history = new LinkedList<IKeypadState>();
            _history.AddLast(new ReadOnlyKeypadState(new ReadOnlyKeypadButtonState[0]));

            _device = device;

            _device.BackingObject.SetupInterface(false);
            _device.BackingObject.SetDataCallback(this, DataCallbackFilterType.callOnNewData);
        }

        #region PIEDataHandler implementation

        void PIEDataHandler.HandlePIEHidData(byte[] data, PIEDevice sourceDevice)
        {
            if (sourceDevice != _device.BackingObject)
            {
                return;
            }


            while (0 == sourceDevice.ReadData(ref data))
            {
                var state = BinaryConversions.ConvertKeypad(data);
                var changes = state.Buttons.Changes(_history.Last.Value.Buttons);
                _history.AddLast(state);

                Buttons = state.Buttons;

                var @event = DeviceEvent.KeypadStateChanged(_device, null);

                _device.AddEvent(@event);
            }
        }

        #endregion
    }
}
