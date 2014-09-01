﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Roomie.Desktop.Engine.Commands;

namespace Roomie.Desktop.Engine
{
    public static class CommandUtilities
    {
        const string Token = ".Commands.";

        public static string GetGroupFromNamespace(string @namespace)
        {
            var start = @namespace.LastIndexOf(Token);

            if (start < 0)
            {
                return null;
            }

            var length = Token.Length;

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

        public static string GetExtensionNameFromNamespace(string @namespace)
        {
            var length = @namespace.LastIndexOf(Token);

            if (length < 0)
            {
                return null;
            }

            var result = @namespace.Substring(0, length);

            return result;
        }

        public static string GetExtensionNameFromNamespace(Type type)
        {
            return GetExtensionNameFromNamespace(type.Namespace);
        }

        public static string GetExtensionNameFromNamespace(this RoomieCommand command)
        {
            return GetExtensionNameFromNamespace(command.GetType());
        }

        public static string GetNameFromType(Type type)
        {
            return type.Name;
        }

        public static string GetNameFromType(this RoomieCommand command)
        {
            return GetNameFromType(command.GetType());
        }

        public static Version GetExtensionVersion(Type type)
        {
            var assembly = Assembly.GetAssembly(type);
            var result = assembly.GetName().Version;

            return result;
        }

        public static Version GetExtensionVersion(this RoomieCommand command)
        {
            return GetExtensionVersion(command.GetType());
        }

        public static string GetExtensionSource(Type type)
        {
            var result = Assembly.GetAssembly(type).CodeBase;

            return result;
        }

        public static string GetExtensionSource(this RoomieCommand command)
        {
            return GetExtensionSource(command.GetType());
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
