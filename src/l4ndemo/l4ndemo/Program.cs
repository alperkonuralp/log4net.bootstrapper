using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace l4ndemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly(),
                 typeof(Hierarchy)) as Hierarchy;

            loggerRepository.ResetConfiguration();

            var ca = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = new PatternLayout("%date %level %message%newline")
            };

            var da = new DebugAppender
            {
                Name = "DebugAppender",
                Layout = new PatternLayout("%date %level %message%newline")
            };

            Logger l = loggerRepository.GetLogger("performance") as Logger;
            l.Additivity = false;
            l.Level = log4net.Core.Level.Error;
            l.AddAppender(ca);

            Logger r = loggerRepository.Root;
            r.Level = log4net.Core.Level.Info;

            r.AddAppender(ca);
            r.AddAppender(da);

            var a = BasicConfigurator.Configure(loggerRepository, ca, da);
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(config));
            //var a = XmlConfigurator.Configure(loggerRepository, ms);

            var logger1 = LogManager.GetLogger("deneme");
            var logger2 = LogManager.GetLogger("performance");

            logger1.Debug("logger1 Debug");
            logger1.Info("logger1 Info");
            logger1.Warn("logger1 Warn");

            logger2.Debug("logger2 Debug");
            logger2.Info("logger2 Info");
            logger2.Warn("logger2 Warn");
            logger2.Error("logger2 Error");
            logger2.Fatal("logger2 Fatal");
        }





        private const string config = @"
<log4net debug=""true"">
  <root>
    <level value=""ALL"" />
    <appender-ref ref=""ConsoleAppender"" />
    <appender-ref ref=""DebugAppender"" />
  </root>
  <logger name=""performans"" additivity=""false"">
    <level value=""ALL"" />
    <appender-ref ref=""ConsoleAppender2"" />
    <appender-ref ref=""DebugAppender2"" />
  </logger>
  <appender name=""ConsoleAppender"" type=""log4net.Appender.ConsoleAppender"">
    <layout type=""log4net.Layout.PatternLayout"">
      <conversionPattern value=""%date %level %message%newline"" />
    </layout>
  </appender>
  <appender name=""DebugAppender"" type=""log4net.Appender.DebugAppender"">
    <layout type=""log4net.Layout.PatternLayout"">
      <conversionPattern value=""%date %level %message%newline"" />
    </layout>
  </appender>
  <appender name=""ConsoleAppender2"" type=""log4net.Appender.ConsoleAppender"">
    <layout type=""log4net.Layout.PatternLayout"">
      <conversionPattern value=""%date %level %message%newline"" />
    </layout>
  </appender>
  <appender name=""DebugAppender2"" type=""log4net.Appender.DebugAppender"">
    <layout type=""log4net.Layout.PatternLayout"">
      <conversionPattern value=""%date %level %message%newline"" />
    </layout>
  </appender>
</log4net>
";
    }
}
