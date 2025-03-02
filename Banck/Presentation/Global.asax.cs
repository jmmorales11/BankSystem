using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using log4net.Config;

namespace Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                // Configurar log4net
                var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
                var log4netConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                XmlConfigurator.Configure(logRepository, new FileInfo(log4netConfigPath));

                ILog log = LogManager.GetLogger(typeof(MvcApplication));
                log.Info("log4net ha sido configurado correctamente en la aplicación MVC.");

                // Configurar MVC
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al configurar log4net: {ex.Message}");
            }
        }
    }
}