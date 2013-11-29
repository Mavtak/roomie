using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.Triggers
{
    public class AndTrigger : ITrigger
    {
        private readonly ITrigger[] _triggers;

        public AndTrigger(IEnumerable<ITrigger> triggers)
        {
            _triggers = triggers.ToArray();
        }

        public AndTrigger(params ITrigger[] triggers)
            : this((IEnumerable<ITrigger>) triggers)
        {
        }

        public bool Check()
        {
            var result = _triggers.All(x => x.Check());

            return result;
        }
    }
}
