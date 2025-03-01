using System;
using System.Configuration;

namespace DAL
{
    public static class ConnectionStringHelper
    {
        public static string GetConnectionString(string connectionName)
        {
            // Leer la contraseña de la variable de entorno
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            // Obtener la cadena base desde App.config
            string baseConnectionString = ConfigurationManager.ConnectionStrings[connectionName]?.ConnectionString;

            if (string.IsNullOrEmpty(baseConnectionString))
            {
                throw new Exception($"No se encontró la cadena de conexión con el nombre '{connectionName}' en el App.config.");
            }

            // Reemplazar el marcador de posición {password_placeholder} con la contraseña
            string updatedConnectionString = baseConnectionString.Replace("{password_placeholder}", password);

            return updatedConnectionString;
        }
    }
}
