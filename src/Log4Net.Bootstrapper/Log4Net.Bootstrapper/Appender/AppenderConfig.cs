using Log4Net.Bootstrapper.Layout;
using System;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Appender
{
    public abstract class AppenderConfig : IConsoleAppenderConfig
    {
        public string Name { get; }
        public ILayoutConfig Layout { get; protected set; } = null;


        protected AppenderConfig(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        public IAppenderConfig SetPatternLayout(string conversionPattern)
        {
            Layout = new PatternLayoutConfig() { ConversionPattern = conversionPattern };
            return this;
        }

        public abstract XElement Generate();


        protected XElement GenerateWithValue(string name, object value)
        {
            return new XElement(name, new XAttribute("value", value));
        }

        protected XElement Generate(string type)
        {
            var el = new XElement("appender",
                new XAttribute("name", Name),
                new XAttribute("type", type));

            if (Layout != null)
            {
                el.Add(Layout.Generate());
            }

            return el;
        }
    }
}