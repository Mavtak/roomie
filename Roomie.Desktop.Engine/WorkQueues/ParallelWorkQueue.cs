using System.Threading;

namespace Roomie.Desktop.Engine.WorkQueues
{
    public class ParallelWorkQueue
    {
        private readonly Thread _thread;
        private readonly WorkQueue _workQueue;
        private readonly AutoResetEvent _event;

        public ParallelWorkQueue()
        {
            _thread = new Thread(Run);
            _workQueue = new WorkQueue();
            _event = new AutoResetEvent(false);
            _thread.Start();
        }

        public void Add(WorkQueueItem work)
        {
            lock (_workQueue)
            {
                _workQueue.Add(work);
                _event.Set();
            }
        }

        public void ShutDown()
        {
            _thread.Abort();
        }

        private void Run()
        {
            while (true)
            {
                _event.WaitOne();

                while (_workQueue.HasWork)
                {
                    _workQueue.DoUnitOfWork();
                }
            }
        }
    }
}
