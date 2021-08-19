using log4net.Appender;
using log4net.Layout;

namespace Log4Net.Bootstrapper.AppenderBuilders
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


        public ConsoleAppenderBuilder Threshold(log4net.Core.Level threshold)
        {
            _consoleAppender.Threshold = threshold;
            return this;
        }
        public ConsoleAppenderBuilder Layout(ILayout layout)
        {
            _consoleAppender.Layout = layout;
            return this;
        }
    }
}