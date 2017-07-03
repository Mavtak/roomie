using System;
using System.Collections.Generic;
using System.Linq;

namespace Roomie.Common.HomeAutomation.Events
{
    public static class EventTypeParser
    {
        public static bool IsValid(string input)
        {
            var @event = TryParse(input);
            var result = @event != null;

            return result;
        }

        public static IEventType TryParse(string input)
        {
            var eventTypes = GetEventTypes();
            var result = eventTypes.FirstOrDefault(x => string.Equals(x.Name, input));

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public static IEventType Parse(string input)
        {
            var result = TryParse(input);

            if (result == null)
            {
                throw new ArgumentException("can not parse Event Type \"" + input + "\".");
            }

            return result;
        }

        public static IEventType ToEventType(this string input)
        {
            return Parse(input);
        }

        private static IEnumerable<IEventType> GetEventTypes()
        {
            var result = GetImplementations<IEventType>()
                .Select(Activator.CreateInstance)
                .Cast<IEventType>();

            return result;
        }

        private static IEnumerable<Type> GetImplementations(Type type)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(x => x.GetTypes());
            var result = types.Where(x => type.IsAssignableFrom(x));
            result = result.Where(x => x != type);

            return result;
        }
 
        public static IEnumerable<Type> GetImplementations<TType>()
        {
            var type = typeof (TType);

            return GetImplementations(type);
        }
    }
}
