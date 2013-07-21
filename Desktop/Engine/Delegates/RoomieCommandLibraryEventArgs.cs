
using System;
namespace Roomie.Desktop.Engine.Delegates
{
    public class RoomieCommandLibraryEventArgs : EventArgs
    {
        public readonly string Message;
        public RoomieCommandLibraryEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
