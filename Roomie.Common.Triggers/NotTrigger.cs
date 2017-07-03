namespace Roomie.Common.Triggers
{
    public class NotTrigger : ITrigger
    {
        private readonly ITrigger _trigger;

        public NotTrigger(ITrigger trigger)
        {
            _trigger = trigger;
        }

        public bool Check()
        {
            var result = !_trigger.Check();

            return result;
        }
    }
}
