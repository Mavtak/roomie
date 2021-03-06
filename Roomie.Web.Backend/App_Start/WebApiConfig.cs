﻿using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Roomie.Web.Backend
{
    public static class WebApiConfig
    {
        public static void ConfigureSerialization(HttpConfiguration config)
        {
            var formatters = config.Formatters;

            formatters.Remove(formatters.XmlFormatter);

            formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                });
        }
    }
}