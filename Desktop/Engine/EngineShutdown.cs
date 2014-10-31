using System;
using System.Threading;

namespace Roomie.Desktop.Engine
{
    //TODO: improve this
    public class EngineShutdown
    {
        private readonly RoomieEngine _engine;
        private ThreadPool _threadpool;
        private Thread _thread;
        public EngineShutdown(RoomieEngine engine)
        {
            _engine = engine;
            _threadpool = _engine.CreateThreadPool("Shutdown Tasks");
        }

        public void Run(Action done)
        {
            if (_thread == null)
            {
                _thread = new Thread(() => Run(done));
                _thread.Start();
                return;
            }

            Print("Shutting Down...");

            RunShutDownTasks();
            WaitForShutDownTasks();
            ShutDownThreadPools();

            Print("Done.");

            done();
        }

        private void RunShutDownTasks()
        {
            foreach (var command in _engine.CommandLibrary.ShutDownTasks)
            {
                Print("Calling " + command.FullName);

                _threadpool.AddCommands(command.BlankCommandCall());
            }
        }

        private void WaitForShutDownTasks()
        {
            //TODO: fix this
            var killTime = DateTime.Now.AddSeconds(10);
            while (_threadpool.IsBusy && DateTime.Now <= killTime)
            {
                Thread.Sleep(100);
            }
        }

        private void ShutDownThreadPools()
        {
            foreach (var threadpool in _engine.ThreadPools)
            {
                threadpool.ShutDown();
            }
        }

        private void Print(string text)
        {
            _threadpool.Print(text);
        }
    }
}
