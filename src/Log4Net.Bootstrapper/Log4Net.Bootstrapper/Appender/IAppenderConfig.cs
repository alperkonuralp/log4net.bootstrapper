using Log4Net.Bootstrapper.Layout;

namespace Log4Net.Bootstrapper.Appender
{
    public interface IAppenderConfig : IGenerator
    {
        string Name { get; }
        ILayoutConfig Layout { get; }

        IAppenderConfig SetPatternLayout(string conversionPattern);

    }
}