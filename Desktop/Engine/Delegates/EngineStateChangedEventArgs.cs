using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roomie.Desktop.Engine.Delegates
{
    public delegate void EngineStateChangedEventHandler(object sender, EngineStateChangedEventArgs e);

    public class EngineStateChangedEventArgs : EventArgs
    {
        public EngineState NewState { get; private set; }
        public EngineState OldState { get; private set; }

        public EngineStateChangedEventArgs(EngineState oldState, EngineState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

    }
}
