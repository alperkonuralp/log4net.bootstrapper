using System;
using System.Text;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Appender
{
    public class RollingLogFileAppenderConfig : AppenderConfig, IRollingLogFileAppenderConfig
    {
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

        public RollingLogFileAppenderConfig(string name, string fileName) : base(name)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            FileName = fileName;
        }

        public string LockingModel { get; private set; } = "log4net.Appender.FileAppender+MinimalLock";
        public Encoding Encoding { get; private set; } = Encoding.UTF8;
        public string FileName { get; private set; }
        public string DatePattern { get; private set; } = "_yyyyMMdd.'log'";
        public bool StaticLogFileName { get; private set; } = false;
        public bool AppendToFile { get; private set; } = true;
        public RollingFileStyle RollingStyle { get; private set; } = RollingFileStyle.Date;
        public string MaxSizeRollBackups { get; private set; } = "100";
        public string MaximumFileSize { get; private set; } = "15MB";


        public IRollingLogFileAppenderConfig SetLockingModel(string lockingModel)
        {
            LockingModel = lockingModel;
            return this;
        }
        public IRollingLogFileAppenderConfig SetEncoding(Encoding encoding)
        {
            Encoding = encoding;
            return this;
        }
        public IRollingLogFileAppenderConfig SetFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            FileName = fileName;
            return this;
        }
        public IRollingLogFileAppenderConfig SetDatePattern(string datePattern)
        {
            DatePattern = datePattern;
            return this;
        }
        public IRollingLogFileAppenderConfig SetStaticLogFileName(bool staticLogFileName)
        {
            StaticLogFileName = staticLogFileName;
            return this;
        }
        public IRollingLogFileAppenderConfig SetAppendToFile(bool appendToFile)
        {
            AppendToFile = appendToFile;
            return this;
        }
        public IRollingLogFileAppenderConfig SetRollingStyle(RollingFileStyle rollingStyle)
        {
            RollingStyle = rollingStyle;
            return this;
        }
        public IRollingLogFileAppenderConfig SetMaxSizeRollBackups(string maxSizeRollBackups)
        {
            MaxSizeRollBackups = maxSizeRollBackups;
            return this;
        }
        public IRollingLogFileAppenderConfig SetMaximumFileSize(string maximumFileSize)
        {
            MaximumFileSize = maximumFileSize;
            return this;
        }

        public override XElement Generate()
        {
            var el = Generate("log4net.Appender.RollingFileAppender");

            el.Add(new XElement("lockingModel", new XAttribute("type", LockingModel)));
            if(Encoding != null)
            {
                el.Add(GenerateWithValue("encoding", this.Encoding.HeaderName));
            }
            el.Add(GenerateWithValue("file", FileName));
            el.Add(GenerateWithValue("datePattern", DatePattern));
            el.Add(GenerateWithValue("staticLogFileName", StaticLogFileName));
            el.Add(GenerateWithValue("appendToFile", AppendToFile));
            el.Add(GenerateWithValue("rollingStyle", RollingStyle.ToString()));

            if (!string.IsNullOrWhiteSpace(MaxSizeRollBackups))
                el.Add(GenerateWithValue("maxSizeRollBackups", MaxSizeRollBackups));

            if (!string.IsNullOrWhiteSpace(MaximumFileSize))
                el.Add(GenerateWithValue("maximumFileSize", MaximumFileSize));

            return el;
        }
    }

}