using System;

namespace Roomie.Common.Triggers.Tests.Fakes
{
    public class OncePerTimespanTriggerFake : OncePerTimespanTrigger
    {
        public OncePerTimespanTriggerFake(ITrigger trigger, TimeSpan timeSpan)
            : base(trigger, timeSpan)
        {
            _now = DateTime.UtcNow;
        }

        private DateTime _now;

        protected override DateTime Now
        {
            get
            {
                return _now;
            }
        }

        public void SetNow(DateTime now)
        {
            _now = now;
        }
    }
}
