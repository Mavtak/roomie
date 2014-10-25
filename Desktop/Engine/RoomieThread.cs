﻿using System.Collections.Generic;
using Roomie.Common.ScriptingLanguage;
using Roomie.Desktop.Engine.WorkQueues;

namespace Roomie.Desktop.Engine
{
    public class RoomieThread
    {
        public readonly RoomieEngine Engine;
        public string Name { get; private set; }
        private readonly RoomieCommandInterpreter _interpreter;
        private readonly ParallelWorkQueue _workQueue;

        public RoomieThread(RoomieEngine engine, string name, RoomieCommandScope parentScope)
        {
            Engine = engine;
            Name = name;
            _interpreter = new RoomieCommandInterpreter(this, parentScope ?? Engine.GlobalScope, Name);
            _workQueue = new ParallelWorkQueue();
        }

        public void ShutDown()
        {
            _workQueue.ShutDown();
            _interpreter.CommandQueue.Clear();
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

            _workQueue.Add(new WorkQueueItem(() => _interpreter.ProcessQueue()));
        }

        public void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            _interpreter.CommandQueue.Add(commands);

            _workQueue.Add(new WorkQueueItem(() => _interpreter.ProcessQueue()));
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
