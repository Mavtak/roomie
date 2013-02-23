using System;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandContext
    {
        //TODO: Should Engine *not* be accessible through here?

        public RoomieCommandInterpreter Interpreter { get; private set; }
        public RoomieCommandScope Scope { get; private set; }
        public IScriptCommand OriginalCommand { get; private set; }

        public RoomieEngine Engine
        {
            get
            {
                return Interpreter.Engine;
            }
        }
        public ArgumentTypeCollection ArgumentTypes
        {
            get
            {
                return Engine.ArgumentTypes;
            }
        }
        public DataStore DataStore
        {
            get
            {
                return Engine.DataStore;
            }
        }
        public RoomieCommandScope GlobalScope
        {
            get
            {
                return Engine.GlobalScope;
            }
        }
        public RoomieCommandLibrary CommandLibrary
        {
            get
            {
                return Engine.CommandLibrary;
            }
        }
        public ThreadPool Threads
        {
            get
            {
                return Engine.Threads;
            }
        }

        internal RoomieCommandContext(RoomieCommandInterpreter interpreter, RoomieCommandScope scope, IScriptCommand originalCommand)
        {
            Interpreter = interpreter;
            Scope = scope;
            OriginalCommand = originalCommand;
        }

        protected RoomieCommandContext(RoomieCommandContext that)
            : this(that.Interpreter, that.Scope, that.OriginalCommand)
        {
        }

        public IScriptCommand GetBlankCommand(Type commandType)
        {
            var command = CommandLibrary.GetCommandFromType(commandType);
            var scriptCommand = command.BlankCommandCall();

            return scriptCommand;
        }
    }
}
