using System;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.Exceptions;

namespace Roomie.Desktop.Engine
{
    public class RoomieCommandInterpreter
    {
        public readonly RoomieThread ParentThread;
        public readonly RoomieCommandScope Scope;
        public readonly ScriptCommandList CommandQueue;
        //TODO: does naming command interpreters even make sense?
        public string Name { get; private set; }
        public bool IsBusy { get; private set; }

        internal RoomieCommandInterpreter(RoomieThread parentThread, RoomieCommandScope parentScope, string name)
        {
            this.ParentThread = parentThread;
            this.Scope = parentScope.CreateLowerScope();
            this.Name = name;

            this.CommandQueue = new ScriptCommandList();
            this.IsBusy = false;
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
                bool success = executeNextCommand();

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

        private bool executeCommand(ScriptCommand languageCommand)
        {
            if (languageCommand.FullName.Equals("RoomieScript"))
            {
                //TODO: just make a "RoomieScript" command?
                this.CommandQueue.AddBeginning(languageCommand.InnerCommands);
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
                commandScope.DeclareVariable(parameter.Name, parameter.Value);

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
            catch (Exception e2)
            {
                WriteEvent("REALLY unexpected error!");
                WriteEvent(e2.ToString());
                CommandQueue.Clear();
                return false;
            }

            return true;
        }

        private bool executeNextCommand()
        {
            ScriptCommand command;
            lock (CommandQueue)
            {
                command = this.CommandQueue.PopFirst();
            }
            return executeCommand(command);
        }

        //This should be wrapped into the CommandLibrary class
        public RoomieCommand ChooseCommand(string commandFullName)
        {
            if (!Engine.CommandLibrary.ContainsCommandFullName(commandFullName))
                throw new CommandNotFoundException(commandFullName);

            return Engine.CommandLibrary.GetCommandFromFullName(commandFullName);
        }

        public void WriteEvent(string message)
        {
            //TODO: depth?
            ParentThread.WriteEvent(message);
        }

        public void ResetLocalScope()
        {
            Scope.ResetLocalScope();
        }

        public RoomieCommandInterpreter GetSubinterpreter()
        {
            return new RoomieCommandInterpreter(ParentThread, Scope.CreateLowerScope(), Name + " Subinterpreter");
        }

    }
}
