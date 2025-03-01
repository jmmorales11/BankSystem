using Newtonsoft.Json;
using Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API
            // Configuración y servicios de Web API

            // Eliminar XML como formato de respuesta
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Configurar JSON como el formato de respuesta por defecto
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented; // Para que el JSON sea legible


            // Registrar el filtro de autenticación JWT
            // Asegúrate de que esta clase esté implementada correctamente


            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new JwtAuthenticationFilter());
        }
    }
}
