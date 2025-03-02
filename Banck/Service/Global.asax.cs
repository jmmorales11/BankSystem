using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using log4net;
using log4net.Config;

namespace Service
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
                var log4netConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigPath));

                ILog log = LogManager.GetLogger(typeof(WebApiApplication));
                log.Info("log4net ha sido configurado correctamente.");

                GlobalConfiguration.Configure(WebApiConfig.Register);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al configurar log4net: {ex.Message}");
            }
        }
    }
}
