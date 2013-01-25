using System.Collections;
using System.Collections.Generic;
using Roomie.Common.ScriptingLanguage;

namespace Roomie.Desktop.Engine
{
    //TODO: make disposible?
    public sealed class ThreadPool : IEnumerable<RoomieThread>
    {
        private RoomieEngine engine;
        string name;
        List<RoomieThread> threads;

        public ThreadPool(RoomieEngine engine, string name)
        {
            this.engine = engine;
            this.name = name;
            this.threads = new List<RoomieThread>();
        }

        public RoomieThread CreateNewThread(string name)
        {
            lock (threads)
            {
                RoomieThread result = new RoomieThread(engine, name);
                lock (threads)
                {
                    threads.Add(result);
                }
                return result;
            }
        }
        public RoomieThread CreateNewThread()
        {
            lock (threads)
            {
                return CreateNewThread(name + " thread " + (threads.Count + 1));
            }
        }

        private RoomieThread getFreeThread()
        {
            foreach (var existingThread in threads)
            {
                if (!existingThread.IsBusy)
                {
                    return existingThread;
                }
            }

            return CreateNewThread();
        }

        public void AddCommand(IScriptCommand command)
        {
            lock (this)
            {
                var thread = getFreeThread();
                
                thread.ResetLocalScope();
                thread.AddCommand(command);
            }
        }

        public void AddCommands(IEnumerable<IScriptCommand> commands)
        {
            lock (this)
            {
                var thread = getFreeThread();

                thread.ResetLocalScope();
                thread.AddCommands(commands);
            }
        }

        public void AddCommands(string text)
        {
            var commands = ScriptCommandList.FromText(text);
            AddCommands(commands);
        }

        public void AddCommands(System.Xml.XmlNode commands)
        {
            AddCommands(commands.OuterXml);
        }

        

        public void Print(string text)
        {
            RoomieThread thread = getFreeThread();
            thread.WriteEvent(text);
        }

        public void ShutDown()
        {
            lock (threads)
            {
                foreach (RoomieThread thread in threads)
                    thread.ShutDown();
                threads.Clear();
            }
        }

        public bool IsBusy
        {
            get
            {
                foreach (var thread in threads)
                    if (thread.IsBusy)
                        return true;
                return false;
            }
        }

        public bool Contains(RoomieThread thread)
        {
            lock (threads)
            {
                return threads.Contains(thread);
            }
        }

        IEnumerator<RoomieThread> IEnumerable<RoomieThread>.GetEnumerator()
        {
            return threads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return threads.GetEnumerator();
        }
    }
}
