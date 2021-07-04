using Log4Net.Bootstrapper.Appender;
using Log4Net.Bootstrapper.Logger;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper
{
    internal class Log4NetConfigurator : ILog4NetConfigurator
    {
        public Dictionary<string, IAppenderConfig> Appenders { get; } = new Dictionary<string, IAppenderConfig>();
        public Dictionary<string, ILogger> Loggers { get; } = new Dictionary<string, ILogger>();
        public IRootLogger Root { get; }
        public bool? Log4NetDebugMode { get; private set; } = null;

        public Log4NetConfigurator()
        {
            Root = new RootLogger(this);
        }

        public IConsoleAppenderConfig AddConsoleAppender(string name)
        {
            if (Appenders.ContainsKey(name) && Appenders[name] is IConsoleAppenderConfig console)
            {
                return console;
            }

            var a = new ConsoleAppenderConfig(name);
            Appenders[name] = a;
            return a;
        }

        public IDebugAppenderConfig AddDebugAppender(string name)
        {
            if (Appenders.ContainsKey(name) && Appenders[name] is IDebugAppenderConfig console)
            {
                return console;
            }

            var a = new DebugAppenderConfig(name);
            Appenders[name] = a;
            return a;
        }

        public IRollingLogFileAppenderConfig AddRollingLogFileAppender(string name, string fileName)
        {
            if (Appenders.ContainsKey(name) && Appenders[name] is IRollingLogFileAppenderConfig console)
            {
                return console;
            }

            var a = new RollingLogFileAppenderConfig(name, fileName);
            Appenders[name] = a;
            return a;
        }


        public ILog4NetConfigurator SetLog4NetDebugMode(bool? debug = null)
        {
            Log4NetDebugMode = debug;
            return this;
        }
        public string GenerateToString()
        {
            XDocument doc = Generate();

            return doc.ToString();
        }

        public XDocument Generate()
        {
            var el = new XElement("log4net");

            if (Log4NetDebugMode.HasValue)
            {
                el.Add(new XAttribute("debug", Log4NetDebugMode));
            }

            el.Add(Root.Generate());

            if (Loggers.Count > 0)
            {
                foreach (var logger in Loggers.Values)
                {
                    el.Add(logger.Generate());
                }
            }

            if (Appenders.Count > 0)
            {
                foreach (var appender in Appenders.Values)
                {
                    el.Add(appender.Generate());
                }
            }

            var xdoc = new XDocument(el);
            return xdoc;
        }

        public ILogger AddLogger(string name)
        {
            if (Loggers.ContainsKey(name))
            {
                return Loggers[name];
            }
            var logger = new Log4Net.Bootstrapper.Logger.Logger(name, this);
            Loggers[name] = logger;
            return logger;
        }

        public ILog4NetConfigurator RemoveAppender(string name)
        {
            if (Appenders.ContainsKey(name))
                Appenders.Remove(name);

            return this;
        }

        public ILog4NetConfigurator RemoveLogger(string name)
        {
            if (Loggers.ContainsKey(name))
                Loggers.Remove(name);

            return this;
        }
    }
}