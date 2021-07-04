using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Appender
{

    public class ConsoleAppenderConfig : AppenderConfig, IConsoleAppenderConfig
    {
        public ConsoleAppenderConfig(string name) : base(name)
        {
        }

        public override XElement Generate() => Generate("log4net.Appender.ConsoleAppender");
    }

}