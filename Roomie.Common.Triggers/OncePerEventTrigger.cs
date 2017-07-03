
namespace Roomie.Common.Triggers
{
    public class OncePerEventTrigger : ITrigger
    {
        private readonly ITrigger _trigger;
        private bool _stuck;

        public OncePerEventTrigger(ITrigger trigger)
        {
            _trigger = trigger;
            _stuck = false;
        }

        public bool Check()
        {
            var actual = _trigger.Check();

            if (!_stuck)
            {
                _stuck = true;
                return actual;
            }

            if (!actual)
            {
                _stuck = false;
            }

            return false;
        }
    }
}
