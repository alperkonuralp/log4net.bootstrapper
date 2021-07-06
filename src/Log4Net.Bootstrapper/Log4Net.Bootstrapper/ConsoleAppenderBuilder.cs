using log4net.Appender;
using log4net.Layout;

namespace Log4Net.Bootstrapper
{
    public class ConsoleAppenderBuilder: IAppenderBuilder
    {
        private ConsoleAppender _consoleAppender;
        public ConsoleAppenderBuilder(string name, string patternLayoutPattern)
        {
            _consoleAppender = new ConsoleAppender
            {
                Name = name
            };
            if(!string.IsNullOrWhiteSpace(patternLayoutPattern))
            {
                _consoleAppender.Layout = new PatternLayout(patternLayoutPattern);
            }
        }

        public IAppender Appender => _consoleAppender;
    }
}