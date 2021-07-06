using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;
using Log4Net.Bootstrapper.Appender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper
{
    internal class Log4NetConfigurator : ILog4NetConfigurator
    {
        private Hierarchy _loggerRepository;
        private ILog4NetBootstrapper _bootstrapper;
        private RootLoggerBuilder _rootLoggerBuilder;

        public Log4NetConfigurator(ILog4NetBootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
            Reset();
        }

        internal Assembly RepositoryAssembly => _bootstrapper.RepositoryAssembly;


        public RootLoggerBuilder Root => _rootLoggerBuilder;



        public ILog4NetConfigurator Reset()
        {
            if (_loggerRepository == null)
                _loggerRepository = LogManager.CreateRepository(RepositoryAssembly ?? Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly(),
                 typeof(Hierarchy)) as Hierarchy;
            else
            {
                _loggerRepository.ResetConfiguration();
                _loggerRepository = LogManager.CreateRepository(RepositoryAssembly ?? Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly(),
                 typeof(Hierarchy)) as Hierarchy;
            }

            _rootLoggerBuilder = new RootLoggerBuilder(this, _loggerRepository);

            return this;
        }

        public ILog4NetConfigurator SetLog4NetDebugMode(bool? debug = null)
        {
            LogLog.InternalDebugging = debug ?? false;
            return this;
        }

        public ConsoleAppenderBuilder CreateConsoleAppender(string name, string patternLayoutPattern = null)
        {
            var cab = new ConsoleAppenderBuilder(name, patternLayoutPattern);

            return cab;
        }
        public DebugAppenderBuilder CreateDebugAppender(string name, string patternLayoutPattern = null)
        {
            var cab = new DebugAppenderBuilder(name, patternLayoutPattern);

            return cab;
        }

        public void Initialize()
        {
            var appenders = new List<IAppender>();

            var rootAppenders = Root.AppenderBuilders.Select(x => x.Appender).ToArray();
            foreach (AppenderSkeleton item in rootAppenders)
            {
                if (appenders.Contains(item)) continue;
                item.ActivateOptions();
                appenders.Add(item);
            }

            var loggers = _loggerRepository.GetCurrentLoggers();
            foreach (Logger logger in loggers)
            {
                foreach (AppenderSkeleton item in logger.Appenders)
                {
                    if (appenders.Contains(item)) continue;
                    item.ActivateOptions();
                    appenders.Add(item);
                }
            }

            BasicConfigurator.Configure(_loggerRepository, rootAppenders);
        }
    }
}