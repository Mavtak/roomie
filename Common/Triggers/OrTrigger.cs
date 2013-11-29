using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.Triggers
{
    public class OrTrigger : ITrigger
    {
        private readonly ITrigger[] _triggers;

        public OrTrigger(IEnumerable<ITrigger> triggers)
        {
            _triggers = triggers.ToArray();
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
