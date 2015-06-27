using System;
using System.Threading;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandInterpreter
    {
        public readonly RoomieThread ParentThread;
        public readonly HierarchicalVariableScope Scope;
        public readonly ScriptCommandList CommandQueue;
        //TODO: does naming command interpreters even make sense?
        public bool IsBusy { get; private set; }

        internal RoomieCommandInterpreter(RoomieThread parentThread, HierarchicalVariableScope parentScope)
        {
            ParentThread = parentThread;
            Scope = parentScope.CreateLowerScope();

            CommandQueue = new ScriptCommandList();
            IsBusy = false;
        }

        internal RoomieEngine Engine
        {
            get
            {
                return ParentThread.Engine;
            }
        }

        public bool ProcessQueue()
        {
            while (CommandQueue.HasCommands)
            {
                IsBusy = true;
                bool success = ExecuteNextCommand();

                if (!success)
                {
                    CommandQueue.Clear();
                    IsBusy = false;
                    return false;
                }
            }
            IsBusy = false;
            return true;
        }

        private bool ExecuteCommand(IScriptCommand languageCommand)
        {
            //TODO: move this check's logic into IScriptCommand
            if (languageCommand.FullName.Equals("RoomieScript"))
            {
                //TODO: just make a "RoomieScript" command?
                CommandQueue.AddBeginning(languageCommand.InnerCommands);
                return true;
            }


            RoomieCommand command;
            try
            {
                command = ChooseCommand(languageCommand.FullName);
            }
            catch (CommandNotFoundException e)
            {
                WriteEvent(e.Message);
                CommandQueue.Clear();
                return false;
            }


            // create a lower scope and populate it with the command arguments
            var commandScope = Scope.CreateLowerScope();
            foreach (var parameter in languageCommand.Parameters)
                commandScope.Local.DeclareVariable(parameter.Name, parameter.Value);

            try
            {
                var context = new RoomieCommandContext
                (
                    interpreter: this,
                    scope: commandScope,
                    originalCommand: languageCommand
                );
                command.Execute(context);
            }
            catch (RoomieRuntimeException e)
            {
                WriteEvent(e.Message);
                CommandQueue.Clear();
                return false;
            }
            catch (NotImplementedException)
            {
                WriteEvent("Command \"" + command.FullName + "\" not implemented.");
                CommandQueue.Clear();
                return false;
            }
            catch(ThreadAbortException e4)
            {
                WriteEvent("Thread shut down.");
                CommandQueue.Clear();
                return false;
            }
            catch (Exception e2)
            {
                WriteEvent("REALLY unexpected error!");
                WriteEvent(e2.ToString());
                CommandQueue.Clear();
                return false;
            }

            return true;
        }

        private bool ExecuteNextCommand()
        {
            var command = CommandQueue.PopFirst();

            return ExecuteCommand(command);
        }

        //This should be wrapped into the CommandLibrary class
        private RoomieCommand ChooseCommand(string commandFullName)
        {
            if (!Engine.CommandLibrary.ContainsCommandFullName(commandFullName))
            {
                throw new CommandNotFoundException(commandFullName);
            }

            return Engine.CommandLibrary.GetCommandFromFullName(commandFullName);
        }

        public void WriteEvent(string message)
        {
            //TODO: depth?
            ParentThread.WriteEvent(message);
        }

        public void ResetLocalScope()
        {
            Scope.Local.Reset();
        }

        public RoomieCommandInterpreter GetSubinterpreter()
        {
            return new RoomieCommandInterpreter(ParentThread, Scope.CreateLowerScope());
        }

    }
}
