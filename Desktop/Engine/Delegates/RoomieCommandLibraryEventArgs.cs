
using System;
namespace Roomie.Desktop.Engine.Delegates
{
    public delegate void RoomieCommandLibraryEventDelegate (object sender, RoomieCommandLibraryEventArgs e);
    public class RoomieCommandLibraryEventArgs : EventArgs
    {
        public readonly string Message;
        public RoomieCommandLibraryEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
