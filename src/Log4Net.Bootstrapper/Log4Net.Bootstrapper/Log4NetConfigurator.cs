using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net.Util;
using Log4Net.Bootstrapper.AppenderBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Log4Net.Bootstrapper
{
    internal class Log4NetConfigurator : ILog4NetConfigurator
    {
        private Hierarchy _loggerRepository;
        private readonly ILog4NetBootstrapper _bootstrapper;
        private RootLoggerBuilder _rootLoggerBuilder;
        private readonly Dictionary<string, IAppenderBuilder> _appenderDictionary =
            new Dictionary<string, IAppenderBuilder>();

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

        private TAppender SetAndReturnAppender<TAppender>(string name, TAppender appender)
            where TAppender : IAppenderBuilder
        {
            _appenderDictionary[name] = appender;
            return appender;
        }

        public ConsoleAppenderBuilder CreateConsoleAppender(string name, string patternLayoutPattern = null)
        {
            return SetAndReturnAppender(name, new ConsoleAppenderBuilder(name, patternLayoutPattern));
        }
        public DebugAppenderBuilder CreateDebugAppender(string name, string patternLayoutPattern = null)
        {
            return SetAndReturnAppender(name, new DebugAppenderBuilder(name, patternLayoutPattern));
        }
        public RollingFileAppenderBuilder CreateRollingFileAppender(string name, string patternLayoutPattern = null)
        {
            return SetAndReturnAppender(name, new RollingFileAppenderBuilder(name, patternLayoutPattern));
        }


        public IAppenderBuilder GetAppender(string name)
        {
            return _appenderDictionary[name];
        }

        public TAppenderBuilder GetAppender<TAppenderBuilder>(string name)
            where TAppenderBuilder: class, IAppenderBuilder
        {
            return _appenderDictionary[name] as TAppenderBuilder;
        }

        public SimpleLayout CreateSimpleLayout(string pattern)
        {
            return new SimpleLayout();
        }
        public PatternLayout CreatePatternLayout(string pattern)
        {
            return new PatternLayout(pattern);
        }

        public DynamicPatternLayout CreateDynamicPatternLayout(string pattern, string header = null, string footer = null)
        {
            DynamicPatternLayout dynamicPatternLayout = new DynamicPatternLayout(pattern);
            if (!string.IsNullOrWhiteSpace(header)) dynamicPatternLayout.Header = header;
            if (!string.IsNullOrWhiteSpace(footer)) dynamicPatternLayout.Footer = footer;
            return dynamicPatternLayout;
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