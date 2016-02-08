using System;

namespace Roomie.CommandDefinitions.SpeechRecognition
{
    public class AutomaticallyResettingSwitch
    {
        private TimeSpan _resetInterval;
        private DateTime? _lastSwitchOn;

        public AutomaticallyResettingSwitch(TimeSpan resetInterval)
        {
            _resetInterval = resetInterval;
        }

        public void SwitchOn()
        {
            _lastSwitchOn = GetNow();
        }

        public bool IsOn
        {
            get
            {
                if (_lastSwitchOn == null)
                {
                    return false;
                }

                if (_lastSwitchOn.Value.Add(_resetInterval) < GetNow())
                {
                    return false;
                }

                return true;
            }
        }

        private DateTime GetNow()
        {
            return DateTime.UtcNow;
        }
    }
}
