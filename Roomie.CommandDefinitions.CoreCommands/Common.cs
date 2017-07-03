using System;

namespace Roomie.CommandDefinitions.CoreCommands
{
    public class Common
    {
        public static Version LibraryVersion
        {
            get
            {
                return InternalLibraryVersion.Get();
            }
        }

        internal static void WaitUntil(DateTime target)
        {
            TimeSpan difference;
            TimeSpan longestSleep = new TimeSpan(0, 0, 5);
            TimeSpan returnTimespan = new TimeSpan(0, 0, 0, 0, 10);
            while (true)
            {
                difference = target.Subtract(DateTime.Now);
                if (difference <= returnTimespan)
                    return;
                if (difference > longestSleep)
                    System.Threading.Thread.Sleep(longestSleep);
                else
                    System.Threading.Thread.Sleep(difference);

            }
        }
    }
}
