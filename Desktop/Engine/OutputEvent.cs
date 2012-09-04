using System;

namespace Roomie.Desktop.Engine
{
    public class OutputEvent
    {
        private DateTime timestamp;
        private RoomieThread thread;
        private string message;

        public OutputEvent(RoomieThread thread, string message)
        {
            this.timestamp = DateTime.Now;
            this.thread = thread;
            this.message = message;
        }

        public DateTime TimeStamp
        {
            get
            {
                return timestamp;
            }
        }

        public DateTime TimeStampUtc
        {
            get
            {
                return timestamp.ToUniversalTime();
            }
        }

        public RoomieThread Thread
        {
            get
            {
                return thread;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }
}
