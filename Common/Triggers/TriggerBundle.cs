using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.Triggers
{
    public class TriggerBundle : ITriggerBundle
    {
        public ITrigger Trigger { get; private set; }
        public IList<ITriggerAction> Actions { get; private set; }

        public TriggerBundle(ITrigger trigger, params ITriggerAction[] actions)
        {
            Trigger = trigger;
            Actions = actions.ToList();
        }
    }
}
