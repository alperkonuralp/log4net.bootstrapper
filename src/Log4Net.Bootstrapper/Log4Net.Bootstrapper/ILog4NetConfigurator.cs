using log4net.Appender;
using log4net.Repository.Hierarchy;
using Log4Net.Bootstrapper.Appender;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
    }
}