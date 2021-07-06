using log4net.Appender;
using log4net.Layout;

namespace Log4Net.Bootstrapper
{
    public class DebugAppenderBuilder : IAppenderBuilder
    {
        private DebugAppender _debugAppender;
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
    }
}