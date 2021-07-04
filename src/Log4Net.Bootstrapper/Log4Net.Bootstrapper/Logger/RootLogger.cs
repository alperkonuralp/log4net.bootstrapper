using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Logger
{

    internal class RootLogger : Logger, IRootLogger
    {
        public RootLogger(ILog4NetConfigurator configurator) : base("root", configurator)
        {
        }

        public override XElement Generate()
        {
            var el = new XElement("root",
                GetLevelElement());

            GetAppenderRefs(el);

            return el;
        }

        public override ILogger SetAdditivity(bool? additivity = null)
        {
            throw new RootLoggerDontSupportAdditivityException();
        }
    }
}