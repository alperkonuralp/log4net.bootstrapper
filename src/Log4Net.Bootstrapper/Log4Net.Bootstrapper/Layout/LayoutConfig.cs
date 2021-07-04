using System.Xml.Linq;

namespace Log4Net.Bootstrapper.Layout
{
    internal abstract class LayoutConfig : ILayoutConfig
    {
        public abstract XElement Generate();
    }

}