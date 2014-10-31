﻿using System;
using System.Threading;

namespace Roomie.Desktop.Engine
{
    //TODO: improve this
    public class EngineShutdown
    {
        private readonly RoomieEngine _engine;
        private Thread _thread;

        public EngineShutdown(RoomieEngine engine)
        {
            _engine = engine;
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

            _engine.Threads.ShutDown(); //TODO: what about other thread pools?
            var shutdownThreads = _engine.CreateThreadPool("Shutdown Tasks");

            foreach (var command in _engine.CommandLibrary.ShutDownTasks)
            {
                Print("Calling " + command.FullName);

                shutdownThreads.AddCommands(command.BlankCommandCall());
            }

            //TODO: fix this
            var killTime = DateTime.Now.AddSeconds(10);
            while (shutdownThreads.IsBusy && DateTime.Now <= killTime)
            {
                Thread.Sleep(100);
            }

            shutdownThreads.ShutDown();

            Print("Done.");

            //this thread will die when the function returns, and all threads will be killed.

            done();
        }

        private void Print(string text)
        {
            _engine.Threads.Print(text);
        }
    }
}
