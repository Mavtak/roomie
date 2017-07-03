using System;

namespace Roomie.Desktop.Engine
{
    public class TerminalOutputEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public TerminalOutputEventArgs(string message)
        {
            this.Message = message;
        }

    }
}
