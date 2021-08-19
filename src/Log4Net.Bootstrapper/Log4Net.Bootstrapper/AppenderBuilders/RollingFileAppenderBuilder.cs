using log4net.Appender;
using log4net.Layout;

namespace Log4Net.Bootstrapper.AppenderBuilders
{
    public class RollingFileAppenderBuilder : IAppenderBuilder
    {
        private readonly RollingFileAppender _rollingFileAppender;
        public RollingFileAppenderBuilder(string name, string patternLayoutPattern)
        {
            _rollingFileAppender = new RollingFileAppender
            {
                Name = name,
                LockingModel = new FileAppender.MinimalLock(),
                DatePattern = "_yyyyMMdd.'log'",
                StaticLogFileName = false,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                MaxSizeRollBackups = 100,
                MaximumFileSize = "15MB"
            };
            if (!string.IsNullOrWhiteSpace(patternLayoutPattern))
            {
                _rollingFileAppender.Layout = new PatternLayout(patternLayoutPattern);
            }
        }
        public IAppender Appender => _rollingFileAppender;

        public RollingFileAppenderBuilder Encoding(string encodingName)
        {
            _rollingFileAppender.Encoding = System.Text.Encoding.GetEncoding(encodingName);
            return this;
        }

        public RollingFileAppenderBuilder Threshold(log4net.Core.Level threshold)
        {
            _rollingFileAppender.Threshold = threshold;
            return this;
        }

        public RollingFileAppenderBuilder Layout(ILayout layout)
        {
            _rollingFileAppender.Layout = layout;
            return this;
        }
        public RollingFileAppenderBuilder LockingModelMinimalLock()
        {
            _rollingFileAppender.LockingModel = new FileAppender.MinimalLock();
            return this;
        }
        public RollingFileAppenderBuilder LockingModelExclusiveLock()
        {
            _rollingFileAppender.LockingModel = new FileAppender.ExclusiveLock();
            return this;
        }
        public RollingFileAppenderBuilder LockingModelInterProcessLock()
        {
            _rollingFileAppender.LockingModel = new FileAppender.InterProcessLock();
            return this;
        }

        public RollingFileAppenderBuilder FileName(string fileName)
        {
            _rollingFileAppender.File = fileName;
            return this;
        }
        public RollingFileAppenderBuilder DatePattern(string datePattern)
        {
            _rollingFileAppender.DatePattern = datePattern;
            return this;
        }
        public RollingFileAppenderBuilder StaticLogFileName(bool staticLogFileName = false)
        {
            _rollingFileAppender.StaticLogFileName = staticLogFileName;
            return this;
        }
        public RollingFileAppenderBuilder AppendToFile(bool appendToFile = true)
        {
            _rollingFileAppender.AppendToFile = appendToFile;
            return this;
        }
        public RollingFileAppenderBuilder RollingStyle(
            RollingFileAppender.RollingMode rollingMode = RollingFileAppender.RollingMode.Date)
        {
            _rollingFileAppender.RollingStyle = rollingMode;
            return this;
        }
        public RollingFileAppenderBuilder MaxSizeRollBackups(int maxSizeRollBackups = 100)
        {
            _rollingFileAppender.MaxSizeRollBackups = maxSizeRollBackups;
            return this;
        }
        public RollingFileAppenderBuilder MaximumFileSize(string maximumFileSize = "15MB")
        {
            _rollingFileAppender.MaximumFileSize = maximumFileSize;
            return this;
        }

        /*
<appender name="RollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
    <lockingModel type="log4net.Appender.FileAppender+MinimalLock"/>
    <encoding value="utf-8" />
    <file value="logs/webapi" />
    <datePattern value="_yyyyMMdd.'log'"/>
    <staticLogFileName value="false"/>
    <appendToFile value="true"/>
    <rollingStyle value="Date"/>
    <maxSizeRollBackups value="100"/>
    <maximumFileSize value="15MB"/>
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%-5p%d{ yyyy-MM-dd HH:mm:ss} [%thread] %m %exception %n%n"/>
    </layout>
  </appender>
         */
    }
}