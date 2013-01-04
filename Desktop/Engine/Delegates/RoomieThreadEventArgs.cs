using System;

namespace Roomie.Desktop.Engine
{
    public delegate void RoomieThreadEventHandler(object sender, RoomieThreadEventArgs e);

    public class RoomieThreadEventArgs : EventArgs
    {
        private OutputEvent scriptMessage;

        public RoomieThreadEventArgs(RoomieThread thread, string message)
            : base()
        {
            this.scriptMessage = new OutputEvent(thread, message);
        }

        public RoomieThread Thread
        {
            get
            {
                return scriptMessage.Thread;
            }
        }

        public string Message
        {
            get
            {
                return scriptMessage.Message;
            }
        }

        public OutputEvent Value
        {
            get
            {
                return scriptMessage;
            }
        }
    }
}
