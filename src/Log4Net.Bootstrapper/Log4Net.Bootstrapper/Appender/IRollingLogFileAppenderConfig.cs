using System.Text;

namespace Log4Net.Bootstrapper.Appender
{
    public interface IRollingLogFileAppenderConfig : IAppenderConfig
    {
        string LockingModel { get; }
        Encoding Encoding { get; }
        string FileName { get; }
        string DatePattern { get; }
        bool StaticLogFileName { get; }
        bool AppendToFile { get; }
        RollingFileStyle RollingStyle { get; }
        string MaxSizeRollBackups { get; }
        string MaximumFileSize { get; }

        IRollingLogFileAppenderConfig SetAppendToFile(bool appendToFile);
        IRollingLogFileAppenderConfig SetDatePattern(string datePattern);
        IRollingLogFileAppenderConfig SetEncoding(Encoding encoding);
        IRollingLogFileAppenderConfig SetFileName(string fileName);
        IRollingLogFileAppenderConfig SetLockingModel(string lockingModel);
        IRollingLogFileAppenderConfig SetMaximumFileSize(string maximumFileSize);
        IRollingLogFileAppenderConfig SetMaxSizeRollBackups(string maxSizeRollBackups);
        IRollingLogFileAppenderConfig SetRollingStyle(RollingFileStyle rollingStyle);
        IRollingLogFileAppenderConfig SetStaticLogFileName(bool staticLogFileName);
    }
}