using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Log4Net.Bootstrapper
{
    public interface IGenerator
    {
        XElement Generate();
    }
}
