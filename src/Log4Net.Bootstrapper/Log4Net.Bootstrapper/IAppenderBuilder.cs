using log4net.Appender;

namespace Log4Net.Bootstrapper
{
    public interface IAppenderBuilder
    {
        IAppender Appender { get; }
    }
}