using System.Collections.Generic;
using System.Threading;
using Roomie.Common.Exceptions;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    public class RoomieThread
    {
        public readonly RoomieEngine Engine;
        public string Name { get; private set; }
        private readonly RoomieCommandInterpreter interpreter;
        private Thread parallelThread;

        public RoomieThread(RoomieEngine engine, string name)
        {
            this.Engine = engine;
            this.Name = name;
            this.interpreter = new RoomieCommandInterpreter(this, this.Engine.GlobalScope, this.Name);
            this.run();
        }

        private void run()
        {
            //the following code block ensures that this method executes in the object's parallel thread
            if (parallelThread == null)
            {
                parallelThread = new System.Threading.Thread(new ThreadStart(run));
                parallelThread.Start();
                return;
            }

            if (Thread.CurrentThread != parallelThread)
            {
                throw new RoomieRuntimeException("Thread \"" + Name + "\" started incorrectly.");
            }

            while (true)
            {
                while (!interpreter.CommandQueue.HasCommands)//TODO: signalling?
                {
                    System.Threading.Thread.Sleep(100);
                }
                interpreter.ProcessQueue();
            }
        }

        public void ShutDown()
        {
            //TODO: more?

            lock (this)
            {
                interpreter.CommandQueue.Clear();

                if (parallelThread != null && parallelThread.IsAlive)
                {
                    parallelThread.Abort();
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return interpreter.IsBusy;
            }
        }

        #region Add Commands
        public void AddCommand(IScriptCommand command)
        {
            interpreter.CommandQueue.Add(command);
        }

        public void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            interpreter.CommandQueue.Add(commands);
        }
        #endregion

        public void ResetLocalScope()
        {
            interpreter.ResetLocalScope();
        }

        public void WriteEvent(string message)
        {
            Engine.SimpleOutputText(this, message);
        }

    }
}
