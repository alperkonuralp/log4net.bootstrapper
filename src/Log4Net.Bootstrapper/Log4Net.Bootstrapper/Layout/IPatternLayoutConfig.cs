namespace Log4Net.Bootstrapper.Layout
{
    public interface IPatternLayoutConfig : ILayoutConfig
    {
        string ConversionPattern { get; set; }
    }

}