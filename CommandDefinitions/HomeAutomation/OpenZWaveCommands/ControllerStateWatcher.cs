using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenZWaveDotNet;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class ControllerStateWatcher : IDisposable
    {
        private readonly OpenZWaveNetwork _network;
        private readonly Queue<ZWControllerState> _states;
        private AutoResetEvent _event;
        public ZWControllerState? LastState { get; private set; }

        public ControllerStateWatcher(OpenZWaveNetwork network)
        {
            _network = network;
            _states = new Queue<ZWControllerState>();
            _event = new AutoResetEvent(false);

            _network.Manager.OnControllerStateChanged += OnControllerStateChanged;
        }

        private void OnControllerStateChanged(ZWControllerState state)
        {
            _network.Log("controller state changed: " + state);

            EnqueueState(state);
        }

        private void EnqueueState(ZWControllerState state)
        {
            lock (_states)
            {
                _states.Enqueue(state);
                LastState = state;
            }

            _event.Set();
        }

        private ZWControllerState? TryDequeueState()
        {
            lock (_states)
            {
                if (_states.Any())
                {
                    return _states.Dequeue();
                }
            }

            return null;
        }

        private ZWControllerState DequeueState()
        {
            while (true)
            {
                var state = TryDequeueState();

                if (state != null)
                {
                    return state.Value;
                }

                _event.WaitOne();
            }
        }

        public void ProcessChanges(Action<ZWControllerState> action = null)
        {
            while (true)
            {
                var state = DequeueState();

                if (action != null)
                {
                    action(state);
                }

                if (FinalState(state))
                {
                    return;
                }
            }
        }

        private static bool FinalState(ZWControllerState state)
        {
            var result = state == ZWControllerState.Completed ||
                         state == ZWControllerState.Failed ||
                         state == ZWControllerState.NodeOK ||
                         state == ZWControllerState.NodeFailed;

            return result;
        }


        public void Dispose()
        {
            _network.Manager.OnControllerStateChanged -= OnControllerStateChanged;
        }
    }
}
