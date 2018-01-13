using System;
using System.Diagnostics;

namespace Roomie.Web.Backend.Helpers
{
    public static class DoWork
    {
        public static void UntilTimeout(int timeout, Func<bool> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalSeconds < timeout)
            {
                var done = action();
                if (done)
                {
                    return;
                }
            }
        }
    }
}
