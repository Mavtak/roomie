using System.Collections.Generic;

namespace Roomie.Common.Triggers
{
    public class AndTrigger : ITrigger
    {
        private readonly List<ITrigger> _triggers;

        public AndTrigger(IEnumerable<ITrigger> triggers)
        {
            _triggers = new List<ITrigger>(triggers);
        }

        public AndTrigger(params ITrigger[] triggers)
            : this((IEnumerable<ITrigger>) triggers)
        {
        }

        public bool Check()
        {
            var result = _triggers.TrueForAll(x => x.Check());

            return result;
        }
    }
}
