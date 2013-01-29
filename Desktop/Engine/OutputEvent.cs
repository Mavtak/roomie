using System;

namespace Roomie.Desktop.Engine
{
    public class OutputEvent
    {
        private readonly DateTime _timestamp;
        private readonly RoomieThread _thread;
        private readonly string _message;

        public OutputEvent(RoomieThread thread, string message)
        {
            _timestamp = DateTime.Now;
            _thread = thread;
            _message = message;
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timestamp;
            }
        }

        public DateTime TimeStampUtc
        {
            get
            {
                return _timestamp.ToUniversalTime();
            }
        }

        public RoomieThread Thread
        {
            get
            {
                return _thread;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
