using System.Reflection;

namespace Log4Net.Bootstrapper
{
    public interface ILog4NetBootstrapper
    {
        ILog4NetConfigurator Configurator { get; }
        Assembly RepositoryAssembly { get; set; }

        void Initialize();
    }
}