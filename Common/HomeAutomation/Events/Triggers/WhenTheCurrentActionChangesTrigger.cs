using System.Linq;

namespace Roomie.Common.HomeAutomation.Events.Triggers
{
    public class WhenTheCurrentActionChangesTrigger : WhenDeviceEventHappensTrigger
    {
        private readonly string _desiredCurrentAction;

        public WhenTheCurrentActionChangesTrigger(IDevice device, string currentAction, IDeviceHistory history)
            : base(device, new CurrentActionChanged(), history)
        {
            _desiredCurrentAction = currentAction;
        }

        protected override System.Collections.Generic.IEnumerable<IDeviceEvent> GetMatches(IDeviceHistory history, IDevice device, IEventType eventType, System.DateTime since)
        {
            var matches = base.GetMatches(history, device, eventType, since);

            matches = matches.Where(x => string.Equals(x.State.CurrentAction, _desiredCurrentAction));

            return matches;
        }
    }
}
