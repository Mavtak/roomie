using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Roomie.Common.Api.Models;

namespace Roomie.Web.Website.Controllers.Api
{
    public static class RpcRequestRouter
    {
        public static object Route(object repository, Request request)
        {
            if (request == null)
            {
                return Response.CreateError("No request specified", "no-request", "programming-error", "invalid-request");
            }

            if (request.Action == null)
            {
                return Response.CreateError("Action not specified", "no-action", "programming-error", "invalid-request");
            }

            var allMethods = repository
                .GetType()
                .GetRuntimeMethods()
                .ToArray();

            if (allMethods.Length == 0)
            {
                return Response.CreateError("There are no actions on this repository", "programming-error", "invalid-request");
            }

            var methodsMatchingActionName = allMethods
                .Where(x => string.Equals(x.Name, request.Action, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (methodsMatchingActionName.Length == 0)
            {
                return Response.CreateError("Action not found that matches the provided name", "programming-error", "no-matching-action-name", "no-matching-action", "invalid-request");
            }

            var providedParameters = request.Parameters ?? new Dictionary<string, object>();

            var methodsMatchingSignature = methodsMatchingActionName
                .Where(x => SignatureMatches(x.GetParameters(), providedParameters))
                .ToArray();

            if (methodsMatchingSignature.Length == 0)
            {
                return Response.CreateError("Action not found that matches the provided signature.", "programming-error", "no-matching-action-signature", "no-matching-action", "invalid-request");
            }

            if (methodsMatchingSignature.Length > 1)
            {
                return Response.CreateError("Multiple actions found that match the provided signature", "programming-error", "multiple-matching-actions", "invalid-request");
            }

            var method = methodsMatchingSignature[0];

            var orderedParameterValues = method.GetParameters()
                .Select(x => GetValue(x, providedParameters))
                .ToArray();

            var result = method.Invoke(repository, orderedParameterValues);

            if (result == null)
            {
                return new Response<object>();
            }

            return result;
        }

        private static object GetValue(ParameterInfo parameter, Dictionary<string, object> providedParameters)
        {
            var providedParameterName = providedParameters
                .Where(x => string.Equals(x.Key, parameter.Name, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Key)
                .FirstOrDefault();

            if (providedParameterName == null)
            {
                return parameter.DefaultValue;
            }

            var value = providedParameters[providedParameterName];

            if (value == null)
            {
                return null;
            }

            return ConvertValue(value, parameter.ParameterType);
        }

        private static bool SignatureMatches(ParameterInfo[] parameters, Dictionary<string, object> providedParameters)
        {
            foreach (var parameter in providedParameters)
            {
                if (!parameters.Any(x => string.Equals(x.Name, parameter.Key)))
                {
                    return false;
                }
            }

            foreach (var parameter in parameters)
            {
                var providedParameterName = providedParameters
                    .Where(x => string.Equals(x.Key, parameter.Name, StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.Key)
                    .FirstOrDefault();

                object value = null;

                if (providedParameterName != null)
                {
                    value = providedParameters[providedParameterName];
                }

                if (!ParameterMatches(parameter, providedParameterName != null, value))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ParameterMatches(ParameterInfo parameter, bool hasValue, object value)
        {
            if (!hasValue && parameter.HasDefaultValue)
            {
                return true;
            }

            if (value == null)
            {
                return parameter.ParameterType.IsClass;
            }

            var providedType = value.GetType();

            if (!TypesMatch(parameter.ParameterType, providedType))
            {
                return false;
            }

            return true;
        }

        private static bool TypesMatch(Type a, Type b)
        {
            if (a == b)
            {
                return true;
            }
            
            if(a.IsArray && b == typeof (JArray))
            {
                //TODO check type of elements in array
                return true;
            }

            if (IsNumber(a) && IsNumber(b))
            {
                return true;
            }

            return false;
        }

        private static bool IsNumber(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private static object ConvertValue(object value, Type type)
        {
            if (value == null)
            {
                return value;
            }

            var valueType = value.GetType();

            if (valueType == type)
            {
                return value;
            }

            if (type == typeof(Int32))
            {
                return Convert.ToInt32(value);
            }

            if(value.GetType() == typeof(JArray))
            {
                var elementType = type.GetElementType();
                var jArray = (JArray)value;
                
                if (elementType == typeof(string))
                {
                    var result = jArray
                        .ToArray()
                        .Select(x => x.ToString())
                        .ToArray();

                    return result;
                }
                else
                {
                    throw new Exception("could not convert array type");
                }
            }

            return value;
        }
    }
}
