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
        private readonly Queue<OpenZWaveNotification> _states;
        private AutoResetEvent _event;

        public ControllerStateWatcher(OpenZWaveNetwork network)
        {
            _network = network;
            _states = new Queue<OpenZWaveNotification>();
            _event = new AutoResetEvent(false);

            _network.Manager.OnNotification += OnNotification;
        }

        private void OnNotification(ZWNotification notification)
        {
            var notification2 = new OpenZWaveNotification(_network, notification);

            EnqueueState(notification2);
        }

        private void EnqueueState(OpenZWaveNotification state)
        {
            lock (_states)
            {
                _states.Enqueue(state);
            }

            _event.Set();
        }

        private OpenZWaveNotification TryDequeueState()
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

        private OpenZWaveNotification DequeueState()
        {
            while (true)
            {
                var state = TryDequeueState();

                if (state != null)
                {
                    return state;
                }

                _event.WaitOne();
            }
        }

        public void ProcessChanges(Func<OpenZWaveNotification, ProcessAction> action)
        {
            while (true)
            {
                var state = DequeueState();

                var processAction = action(state);

                if (processAction == ProcessAction.Quit)
                {
                    return;
                }
            }
        }

        public void WaitUntilEventType(params ZWNotification.Type[] finalTypes)
        {
            ProcessChanges(notification =>
            {
                if (finalTypes.Contains(notification.Type))
                {
                    return ProcessAction.Quit;
                }

                return ProcessAction.Continue;
            });
        }

        public void LogChangesForever()
        {
            ProcessChanges(notification =>
            {
                _network.Log("controller state changed: " + notification.Type + ", " + notification.NodeId);

                return ProcessAction.Continue;
            });
        }

        public void Dispose()
        {
            _network.Manager.OnNotification -= OnNotification;
        }

        public enum ProcessAction
        {
            Continue,
            Quit
        }
    }
}
