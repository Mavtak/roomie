using System;

namespace Roomie.Desktop.Engine.Delegates
{
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
