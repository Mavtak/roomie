using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.Triggers
{
    public class OrTrigger : ITrigger
    {
        private readonly List<ITrigger> _triggers;

        public OrTrigger(IEnumerable<ITrigger> triggers)
        {
            _triggers = new List<ITrigger>(triggers);
        }

        public OrTrigger(params ITrigger[] triggers)
            : this((IEnumerable<ITrigger>) triggers)
        {
        }

        public bool Check()
        {
            var result = _triggers.Any(x => x.Check());

            return result;
        }
    }
}
