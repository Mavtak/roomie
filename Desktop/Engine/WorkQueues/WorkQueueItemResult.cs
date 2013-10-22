using System;

namespace Roomie.Desktop.Engine.WorkQueues
{
    public class WorkQueueItemResult
    {
        public bool Success { get; private set; }
        public uint Tries { get; private set; }
        public Exception Exception { get; private set; }

        public bool ShouldRetry
        {
            get
            {
                var result = !Success && Tries > 0;

                return result;
            }
        }

        public WorkQueueItemResult(bool succes, uint tries, Exception exception = null)
        {
            Success = succes;
            Tries = tries;
            Exception = exception;
        }
    }
}
