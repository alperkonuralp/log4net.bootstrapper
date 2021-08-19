using log4net.Appender;
using log4net.Layout;

namespace Log4Net.Bootstrapper.AppenderBuilders
{
    public class DebugAppenderBuilder : IAppenderBuilder
    {
        private readonly DebugAppender _debugAppender;
        public DebugAppenderBuilder(string name, string patternLayoutPattern)
        {
            _debugAppender = new DebugAppender
            {
                Name = name
            };
            if (!string.IsNullOrWhiteSpace(patternLayoutPattern))
            {
                _debugAppender.Layout = new PatternLayout(patternLayoutPattern);
            }
        }
        public IAppender Appender => _debugAppender;


        public DebugAppenderBuilder Threshold(log4net.Core.Level threshold)
        {
            _debugAppender.Threshold = threshold;
            return this;
        }

        public DebugAppenderBuilder Layout(ILayout layout)
        {
            _debugAppender.Layout = layout;
            return this;
        }
    }
}