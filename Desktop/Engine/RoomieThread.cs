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
        private readonly RoomieCommandInterpreter _interpreter;
        private Thread _parallelThread;

        public RoomieThread(RoomieEngine engine, string name)
        {
            Engine = engine;
            Name = name;
            _interpreter = new RoomieCommandInterpreter(this, this.Engine.GlobalScope, this.Name);
            Run();
        }

        private void Run()
        {
            //the following code block ensures that this method executes in the object's parallel thread
            if (_parallelThread == null)
            {
                _parallelThread = new System.Threading.Thread(new ThreadStart(Run));
                _parallelThread.Start();
                return;
            }

            if (Thread.CurrentThread != _parallelThread)
            {
                throw new RoomieRuntimeException("Thread \"" + Name + "\" started incorrectly.");
            }

            while (true)
            {
                while (!_interpreter.CommandQueue.HasCommands)//TODO: signalling?
                {
                    Thread.Sleep(100);
                }
                _interpreter.ProcessQueue();
            }
        }

        public void ShutDown()
        {
            //TODO: more?

            lock (this)
            {
                _interpreter.CommandQueue.Clear();

                if (_parallelThread != null && _parallelThread.IsAlive)
                {
                    _parallelThread.Abort();
                }
            }
        }

        public bool IsBusy
        {
            get
            {
                return _interpreter.IsBusy;
            }
        }

        #region Add Commands

        public void AddCommand(IScriptCommand command)
        {
            _interpreter.CommandQueue.Add(command);
        }

        public void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            _interpreter.CommandQueue.Add(commands);
        }

        #endregion

        public void ResetLocalScope()
        {
            _interpreter.ResetLocalScope();
        }

        public void WriteEvent(string message)
        {
            Engine.SimpleOutputText(this, message);
        }

    }
}
