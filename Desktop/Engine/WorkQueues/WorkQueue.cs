using System.Collections.Generic;

namespace Roomie.Desktop.Engine.WorkQueues
{
    public class WorkQueue
    {
        private readonly Queue<WorkQueueItem> _workQueue;

        public WorkQueue()
        {
            _workQueue = new Queue<WorkQueueItem>();
        }

        public void Add(WorkQueueItem work)
        {
            lock (_workQueue)
            {
                _workQueue.Enqueue(work);
            }
        }

        public void DoUnitOfWork()
        {
            WorkQueueItem work;

            lock (_workQueue)
            {
                if (!HasWork)
                {
                    return;
                }

                work = _workQueue.Dequeue();
            }

            var result = work.DoWork();

            if (!result.ShouldRetry)
            {
                return;
            }

            lock (_workQueue)
            {
                _workQueue.Enqueue(work);
            }
        }

        public bool HasWork
        {
            get
            {
                bool result;

                lock (_workQueue)
                {
                    result = _workQueue.Count > 0;
                }

                return result;
            }
        }
    }
}
