using System.Collections.Generic;

namespace Roomie.Common.Triggers
{
    public interface ITriggerCollection : IEnumerable<ITriggerBundle>
    {
        void Add(ITriggerBundle triggerBundle);
        void Remove(ITriggerBundle triggerBundle);
        IEnumerable<ITriggerBundle> Check();
    }
}
