
namespace Roomie.Common.HomeAutomation.Events
{
    public static class EventTypeExtensions
    {
        public static bool Matches(this IEventType a, IEventType b)
        {
            var aType = a.GetType();
            var bType = b.GetType();

            var result = aType == bType;

            return result;
        }
    }
}
