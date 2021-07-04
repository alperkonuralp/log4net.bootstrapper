namespace Log4Net.Bootstrapper
{
    public interface ILog4NetBootstrapper
    {
        ILog4NetConfigurator Configurator { get; }

        void Initialize();
    }
}