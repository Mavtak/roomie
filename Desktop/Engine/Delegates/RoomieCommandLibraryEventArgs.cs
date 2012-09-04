using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.Delegates
{
    public delegate void RoomieCommandLibraryEventDelegate (object sender, RoomieCommandLibraryEventArgs eventArgs);
    public class RoomieCommandLibraryEventArgs
    {
        public readonly string Message;
        public RoomieCommandLibraryEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
