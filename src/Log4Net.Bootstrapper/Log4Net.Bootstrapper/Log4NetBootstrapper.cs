using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Log4Net.Bootstrapper
{
    public class Log4NetBootstrapper : ILog4NetBootstrapper
    {
        public ILog4NetConfigurator Configurator { get; } 
        public Assembly RepositoryAssembly { get; set; } = null;

        public Log4NetBootstrapper()
        {
            Configurator = new Log4NetConfigurator(this);
        }

        public ILog4NetBootstrapper SetRepositoryAssembly(Assembly assembly)
        {
            RepositoryAssembly = assembly;
            return this;
        }
        public ILog4NetBootstrapper SetRepositoryAssembly<T>()
        {
            RepositoryAssembly = typeof(T).Assembly;
            return this;
        }

        public void Initialize()
        {
            ////XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo(fn));
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(Configurator.GenerateToString()));
            //ms.Seek(0, SeekOrigin.Begin);
            //XmlConfigurator.Configure(_loggerRepository, ms);
            Configurator.Initialize();
        }
    }
}
