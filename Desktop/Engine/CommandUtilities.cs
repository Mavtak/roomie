using System;
using System.Collections.Generic;
using System.Linq;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine
{
    public static class CommandUtilities
    {
        public static string GetGroupFromNamespace(Type type)
        {
            try
            {
                string result = type.Namespace;
                result = result.Substring(result.LastIndexOf(".Commands.") + ".Commands.".Length);
                if (string.IsNullOrEmpty(result))
                    throw new Exception("just goin' to the catch block"); //TODO: review this.  It seems awful
                return result;
            }
            catch
            {
                //TODO: What kind of exception should be thrown here?
                throw new Exception("Command " + GetNameFromType(type) + "'s namespace is not in the proper form");
            }
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
