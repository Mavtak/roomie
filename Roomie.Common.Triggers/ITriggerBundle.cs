using System.Collections.Generic;

namespace Roomie.Common.Triggers
{
    public interface ITriggerBundle
    {
        ITrigger Trigger { get; }
        IList<ITriggerAction> Actions { get; }
    }
}
