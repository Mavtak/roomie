using System;
using System.Linq;
using System.Threading;

namespace Roomie.Desktop.Engine.WorkQueues
{
    public class WorkQueueItem
    {
        private readonly Action _work;
        private WorkQueueItemResult _result;
        private readonly Type[] _retryExceptions;
        private uint _tries;
        private ManualResetEvent _event;

        public WorkQueueItemResult Result
        {
            get
            {
                WaitUntilComplete();

                return _result;
            }
        }

        public WorkQueueItem(Action work, uint tries, params Type[] retryExceptions)
        {
            _work = work;
            _tries = tries;
            _retryExceptions = retryExceptions ?? new Type[0];
            _event = new ManualResetEvent(false);

        }

        public WorkQueueItem(Action work)
            : this(work, 1, null)
        {
        }

        public void WaitUntilComplete()
        {
            _event.WaitOne();
        }

        public WorkQueueItemResult DoWork()
        {
            WorkQueueItemResult result;

            try
            {
                _work();

                result = new WorkQueueItemResult(true, _tries);
            }
            catch (Exception exception)
            {
                var exceptionType = exception.GetType();

                _tries--;

                if (_retryExceptions.Any(x => x.IsAssignableFrom(exceptionType)))
                {
                    result = new WorkQueueItemResult(false, _tries, exception);   
                }
                else
                {
                    result = new WorkQueueItemResult(false, 0, exception);
                }
            }

            if (!result.ShouldRetry)
            {
                _result = result;
                _event.Set();
            }
            
            return result;
        }

    }
}
