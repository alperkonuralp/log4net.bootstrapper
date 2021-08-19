using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;

namespace Log4Net.Bootstrapper
{
    public class RootLoggerBuilder
    {
        private readonly ILog4NetConfigurator _log4NetConfigurator;
        private readonly Hierarchy _loggerRepository;
        private readonly List<IAppenderBuilder> _appenderBuilders = new List<IAppenderBuilder>();

        internal RootLoggerBuilder(Log4NetConfigurator log4NetConfigurator, Hierarchy loggerRepository)
        {
            this._log4NetConfigurator = log4NetConfigurator;
            this._loggerRepository = loggerRepository;
        }

        internal List<IAppenderBuilder> AppenderBuilders => _appenderBuilders;

        public RootLoggerBuilder Level(Level level)
        {
            _loggerRepository.Root.Level = level;
            return this;
        }

        public RootLoggerBuilder AddAppender(IAppenderBuilder appender)
        {
            _appenderBuilders.Add(appender);
            return this;
        }

        public RootLoggerBuilder AddAppender(string appenderName)
        {
            var appender = _log4NetConfigurator.GetAppender(appenderName);
            return AddAppender(appender);
        }
    }
}