using System;

namespace Roomie.Common.Triggers
{
    public class OncePerTimespanTrigger : ITrigger
    {
        private readonly ITrigger _trigger;
        private readonly TimeSpan _timeSpan;
        private DateTime? _lastTrigger;

        public OncePerTimespanTrigger(ITrigger trigger, TimeSpan timeSpan)
        {
            _trigger = trigger;
            _timeSpan = timeSpan;
        }

        public bool Check()
        {
            var now = Now;

            if (!CanTrigger(now))
            {
                return false;
            }

            var result = _trigger.Check();

            if (result)
            {
                UpdateLastTrigger(now);
            }

            return result;
        }

        protected virtual DateTime Now 
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        private bool CanTrigger(DateTime now)
        {
            bool result;

            if (_lastTrigger.HasValue)
            {
                var timeSinceLastTrigger = now.Subtract(_lastTrigger.Value);

                result = timeSinceLastTrigger > _timeSpan;
            }
            else
            {
                result = true;
            }

            return result;
        }

        private void UpdateLastTrigger(DateTime now)
        {
            _lastTrigger = now;
        }
    }
}
