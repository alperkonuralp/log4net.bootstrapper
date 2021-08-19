using Log4Net.Bootstrapper.AppenderBuilders;

namespace Log4Net.Bootstrapper
{
    public interface ILog4NetConfigurator
    {
        RootLoggerBuilder Root { get; }

        ILog4NetConfigurator SetLog4NetDebugMode(bool? debug = null);
        ILog4NetConfigurator Reset();


        ConsoleAppenderBuilder CreateConsoleAppender(string name, string patternLayoutPattern = null);
        DebugAppenderBuilder CreateDebugAppender(string name, string patternLayoutPattern = null);
        void Initialize();

        IAppenderBuilder GetAppender(string name);
        TAppenderBuilder GetAppender<TAppenderBuilder>(string name) 
            where TAppenderBuilder : class, IAppenderBuilder;
        RollingFileAppenderBuilder CreateRollingFileAppender(string name, string patternLayoutPattern = null);
    }
}