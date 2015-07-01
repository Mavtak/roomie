using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenZWaveDotNet;

namespace Roomie.CommandDefinitions.OpenZWaveCommands
{
    public class ControllerNotificationWatcher : IDisposable
    {
        private readonly OpenZWaveNetwork _network;
        private readonly Queue<OpenZWaveNotification> _notification;
        private AutoResetEvent _event;

        public ControllerNotificationWatcher(OpenZWaveNetwork network)
        {
            _network = network;
            _notification = new Queue<OpenZWaveNotification>();
            _event = new AutoResetEvent(false);

            _network.Manager.OnNotification += OnNotification;
        }

        private void OnNotification(ZWNotification notification)
        {
            var notification2 = new OpenZWaveNotification(_network, notification);

            EnqueueNotification(notification2);
        }

        private void EnqueueNotification(OpenZWaveNotification notification)
        {
            lock (_notification)
            {
                _notification.Enqueue(notification);
            }

            _event.Set();
        }

        private OpenZWaveNotification TryDequeueNotification()
        {
            lock (_notification)
            {
                if (_notification.Any())
                {
                    return _notification.Dequeue();
                }
            }

            return null;
        }

        private OpenZWaveNotification DequeueNotification()
        {
            while (true)
            {
                var notification = TryDequeueNotification();

                if (notification != null)
                {
                    return notification;
                }

                _event.WaitOne();
            }
        }

        public void ProcessChanges(Func<OpenZWaveNotification, ProcessAction> action)
        {
            while (true)
            {
                var notification = DequeueNotification();

                var processAction = action(notification);

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
                _network.Log("controller notification: " + notification.Type + ", " + notification.NodeId);

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
