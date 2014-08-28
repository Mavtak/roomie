using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine
{
    public static class CommandUtilities
    {
        public static string GetGroupFromNamespace(string @namespace)
        {
            const string token = ".Commands.";

            var start = @namespace.LastIndexOf(token);

            if (start < 0)
            {
                return null;
            }

            var length = token.Length;

            var result = @namespace.Substring(start + length);

            return result;
        }

        public static string GetGroupFromNamespace(Type type)
        {
            return GetGroupFromNamespace(type.Namespace);
        }

        public static string GetGroupFromNamespace(this RoomieCommand command)
        {
            return GetGroupFromNamespace(command.GetType());
        }

        public static string GetNameFromType(Type type)
        {
            return type.Name;
        }

        public static string GetNameFromType(this RoomieCommand command)
        {
            return GetNameFromType(command.GetType());
        }

        public static string GetDescriptionFromAttributes(Type type)
        {
            var descriptionAttribute = type.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            if (descriptionAttribute == null)
            {
                return null;
            }

            return descriptionAttribute.Value;
        }

        public static string GetDescriptionFromAttributes(this RoomieCommand command)
        {
            return GetDescriptionFromAttributes(command.GetType());
        }

        public static IEnumerable<RoomieCommandArgument> GetArgumentsFromAttributes(Type type)
        {
            foreach (ParameterAttribute parameter in type.GetCustomAttributes(typeof(ParameterAttribute), true))
            {
                var argument = new RoomieCommandArgument(
                    name: parameter.Name,
                    type: parameter.Type,
                    defaultValue: parameter.Default,
                    hasDefault: parameter.HasDefault
                    );

                yield return argument;
            }
        }

        public static IEnumerable<RoomieCommandArgument> GetArgumentsFromAttributes(this RoomieCommand command)
        {
            return GetArgumentsFromAttributes(command.GetType());
        }
    }
}
