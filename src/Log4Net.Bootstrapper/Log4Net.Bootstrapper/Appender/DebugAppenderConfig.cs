using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Appender
{
    public class DebugAppenderConfig : AppenderConfig, IDebugAppenderConfig
    {
        public DebugAppenderConfig(string name) : base(name)
        {
        }

        public override XElement Generate() => Generate("log4net.Appender.DebugAppender");
    }
}