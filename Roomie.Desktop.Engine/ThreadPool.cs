﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    public sealed class ThreadPool : IEnumerable<RoomieThread>
    {
        private readonly RoomieEngine _engine;
        readonly string _name;
        readonly List<RoomieThread> _threads;

        internal ThreadPool(RoomieEngine engine, string name)
        {
            _engine = engine;
            _name = name;
            _threads = new List<RoomieThread>();
        }

        public RoomieThread CreateNewThread(string name = null, HierarchicalVariableScope parentScope = null)
        {
            lock (_threads)
            {
                if (name == null)
                {
                    name = _name + " thread " + (_threads.Count + 1);
                }

                var result = new RoomieThread(_engine, name, parentScope);
                
                _threads.Add(result);

                return result;
            }
        }

        private RoomieThread GetFreeThread()
        {
            foreach (var existingThread in _threads)
            {
                if (!existingThread.IsBusy)
                {
                    return existingThread;
                }
            }

            return CreateNewThread();
        }

        public void AddCommands(params IScriptCommand[] commands)
        {
            AddCommands(commands.AsEnumerable());
        }

        public void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            lock (_threads)
            {
                var thread = GetFreeThread();

                thread.ResetLocalScope();
                thread.AddCommands(commands);
            }
        }

        public void Print(string text)
        {
            var thread = GetFreeThread();

            thread.WriteEvent(text);
        }

        public void ShutDown()
        {
            lock (_threads)
            {
                foreach (var thread in _threads)
                {
                    thread.ShutDown();
                }

                _threads.Clear();
            }
        }

        public bool IsBusy
        {
            get
            {
                return _threads.Any(thread => thread.IsBusy);
            }
        }

        public bool Contains(RoomieThread thread)
        {
            lock (_threads)
            {
                return _threads.Contains(thread);
            }
        }

        IEnumerator<RoomieThread> IEnumerable<RoomieThread>.GetEnumerator()
        {
            return _threads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _threads.GetEnumerator();
        }
    }
}
