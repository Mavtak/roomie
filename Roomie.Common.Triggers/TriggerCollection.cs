using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.Triggers
{
    public class TriggerCollection : ITriggerCollection
    {
        private readonly List<ITriggerBundle> _bundles;

        public TriggerCollection()
        {
            _bundles = new List<ITriggerBundle>();
        }

        public void Add(ITriggerBundle triggerBundle)
        {
            lock (this)
            {
                _bundles.Add(triggerBundle);
            }
        }

        public void Remove(ITriggerBundle triggerBundle)
        {
            lock (this)
            {
                _bundles.Remove(triggerBundle);
            }
        }

        public IEnumerable<ITriggerBundle> Check()
        {
            var results = _bundles.Where(bundle => bundle.Trigger.Check());

            lock (this)
            {
                results = results.ToArray();
            }

            return results;
        }

        #region IEnumerable

        public IEnumerator<ITriggerBundle> GetEnumerator()
        {
            return _bundles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
