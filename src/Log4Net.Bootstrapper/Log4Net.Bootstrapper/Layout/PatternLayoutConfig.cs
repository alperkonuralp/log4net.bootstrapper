using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Layout
{
    internal class PatternLayoutConfig : LayoutConfig, IPatternLayoutConfig
    {
        public string ConversionPattern { get; set; }

        public override XElement Generate()
        {
            var el = new XElement("layout",
                new XAttribute("type", "log4net.Layout.PatternLayout"),
                new XElement("conversionPattern", new XAttribute("value", ConversionPattern))
                );

            return el;
        }
    }

}