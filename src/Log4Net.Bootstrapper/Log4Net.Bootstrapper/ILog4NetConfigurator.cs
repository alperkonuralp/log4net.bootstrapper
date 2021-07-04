using Log4Net.Bootstrapper.Appender;
using Log4Net.Bootstrapper.Logger;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper
{
    public interface ILog4NetConfigurator
    {
        IRootLogger Root { get; }
        bool? Log4NetDebugMode { get; }

        Dictionary<string, IAppenderConfig> Appenders { get; }
        Dictionary<string, ILogger> Loggers { get; }

        IConsoleAppenderConfig AddConsoleAppender(string name);

        IDebugAppenderConfig AddDebugAppender(string name);

        IRollingLogFileAppenderConfig AddRollingLogFileAppender(string name, string fileName);

        ILog4NetConfigurator RemoveAppender(string name);
        ILog4NetConfigurator RemoveLogger(string name);

        string GenerateToString();

        XDocument Generate();
        ILogger AddLogger(string name);

        ILog4NetConfigurator SetLog4NetDebugMode(bool? debug = null);
    }
}