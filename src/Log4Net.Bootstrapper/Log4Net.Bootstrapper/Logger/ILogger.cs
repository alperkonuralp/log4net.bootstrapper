using log4net.Core;
using Log4Net.Bootstrapper.Appender;
using System;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Logger
{
    public interface ILogger : IGenerator
    {
        string Name { get; }
        ILog4NetConfigurator Configurator { get; }

        bool? Additivity { get; }

        ILogger Level(Level level);
        ILogger AppenderRef(string appenderRef);
        ILogger RemoveAppenderRef(string appenderRef);
        ILogger SetAdditivity(bool? additivity = null);

        ILogger AddConsoleAppender(string name, Action<IConsoleAppenderConfig> consoleAppenderConfigurator);
        ILogger AddDebugAppender(string name, System.Action<IDebugAppenderConfig> debugAppenderConfigurator);
        ILogger AddRollingLogFileAppenderConfig(string name, string fileName, System.Action<IRollingLogFileAppenderConfig> rollingLogFileAppenderConfigConfigurator);
    }
}