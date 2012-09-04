using System;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandContext
    {
        //TODO: Should engine.GlobalScope be accessible through here?
        //TODO: Should engine.Threads be accessible through here?
        //TODO: Should Engine *not* be accessible through here?

        public RoomieCommandInterpreter Interpreter { get; private set; }
        public RoomieCommandScope Scope { get; private set; }
        public ScriptCommand OriginalCommand { get; private set; }

        public RoomieEngine Engine
        {
            get
            {
                return Interpreter.Engine;
            }
        }
        public DataStore DataStore
        {
            get
            {
                return Engine.DataStore;
            }
        }

        public RoomieCommandLibrary CommandLibrary
        {
            get
            {
                return Engine.CommandLibrary;
            }
        }

        internal RoomieCommandContext(RoomieCommandInterpreter interpreter, RoomieCommandScope scope, ScriptCommand originalCommand)
        {
            this.Interpreter = interpreter;
            this.Scope = scope;
            this.OriginalCommand = originalCommand;
        }

        protected RoomieCommandContext(RoomieCommandContext that)
            : this(that.Interpreter, that.Scope, that.OriginalCommand)
        { }

        public ScriptCommand GetBlankCommand(Type commandType)
        {
            var command = CommandLibrary.GetCommandFromType(commandType);
            var scriptCommand = command.BlankCommandCall();

            return scriptCommand;
        }
    }
}
