using log4net.Core;
using Log4Net.Bootstrapper.Appender;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Logger
{
    internal class Logger : ILogger
    {

        protected Level _level = log4net.Core.Level.All;
        protected List<string> _appenderRefs = new List<string>();

        public Logger(string name, ILog4NetConfigurator configurator)
        {
            Name = name;
            Configurator = configurator;
        }

        public string Name { get; }
        public ILog4NetConfigurator Configurator { get; }

        public bool? Additivity { get; private set; } = null;

        public ILogger AppenderRef(string appenderRef)
        {
            if (!_appenderRefs.Contains(appenderRef))
            {
                _appenderRefs.Add(appenderRef);
            }
            return this;
        }
        public ILogger RemoveAppenderRef(string appenderRef)
        {
            if (_appenderRefs.Contains(appenderRef))
            {
                _appenderRefs.Remove(appenderRef);
            }
            return this;
        }

        public virtual XElement Generate()
        {
            var el = new XElement("logger",
                new XAttribute("name", Name)
                );

            if (Additivity.HasValue)
            {
                el.Add(new XAttribute("additivity", Additivity.Value));
            }

            el.Add(GetLevelElement());
            GetAppenderRefs(el);

            return el;
        }

        public ILogger Level(Level level)
        {
            _level = level;
            return this;
        }

        public ILogger AddConsoleAppender(string name, System.Action<IConsoleAppenderConfig> consoleAppenderConfigurator)
        {
            var console = Configurator.AddConsoleAppender(name);
            consoleAppenderConfigurator(console);

            return AppenderRef(name);
        }


        public ILogger AddDebugAppender(string name, System.Action<IDebugAppenderConfig> debugAppenderConfigurator)
        {
            var appender = Configurator.AddDebugAppender(name);
            debugAppenderConfigurator(appender);

            return AppenderRef(name);
        }

        public ILogger AddRollingLogFileAppenderConfig(string name, string fileName, System.Action<IRollingLogFileAppenderConfig> rollingLogFileAppenderConfigConfigurator)
        {
            var appender = Configurator.AddRollingLogFileAppender(name, fileName);
            rollingLogFileAppenderConfigConfigurator(appender);

            return AppenderRef(name);
        }


        public virtual ILogger SetAdditivity(bool? additivity = null)
        {
            Additivity = additivity;
            return this;
        }

        protected XElement GetLevelElement() => new XElement("level", new XAttribute("value", _level.Name));
        protected void GetAppenderRefs(XElement el)
        {
            foreach (var item in _appenderRefs)
            {
                el.Add(new XElement("appender-ref", new XAttribute("ref", item)));
            }
        }

    }
}