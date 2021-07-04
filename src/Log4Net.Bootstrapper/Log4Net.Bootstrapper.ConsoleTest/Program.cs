using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;

namespace Log4Net.Bootstrapper.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog4NetBootstrapper bootstrapper = new Log4NetBootstrapper();

            bootstrapper.Configurator
                .SetLog4NetDebugMode(true)
                .Root
                    .Level(Level.All)
                    .AddConsoleAppender("ConsoleAppender", c=> c.SetPatternLayout("%date %level %message%newline"))
                    .AddDebugAppender("DebugAppender", c => c.SetPatternLayout("%date %level %message%newline"))
                    .AddRollingLogFileAppenderConfig("RollingLogFileAppender", "logs/webapi", 
                        c=>c.SetDatePattern("_yyyyMMddHH.'log'")
                            .SetPatternLayout("%-5p%d{ yyyy-MM-dd HH:mm:ss} [%thread] %m %exception %n%n"));

            bootstrapper.Configurator
                .AddLogger("performans")
                    .SetAdditivity(false)
                    .Level(Level.All)
                    .AddConsoleAppender("ConsoleAppender2", c => c.SetPatternLayout("%date %level %message%newline"))
                    .AddDebugAppender("DebugAppender2", c => c.SetPatternLayout("%date %level %message%newline"));


            //bootstrapper.Configurator
            //    .AddConsoleAppender("ConsoleAppender")
            //        .SetPatternLayout("%date %level %message%newline");

            //bootstrapper.Configurator
            //    .AddDebugAppender("DebugAppender")
            //        .SetPatternLayout("%date %level %message%newline");


            string s = bootstrapper.Configurator.GenerateToString();

            Console.WriteLine(s);

            bootstrapper.Initialize();

            var logger = LogManager.GetLogger("default");
            logger.Debug("Merhaba");


            bootstrapper.Configurator
                .SetLog4NetDebugMode(false)
                .Root
                    .RemoveAppenderRef("DebugAppender")
                    .RemoveAppenderRef("RollingLogFileAppender")
                    ;


            s = bootstrapper.Configurator.GenerateToString();

            Console.WriteLine(s);

            bootstrapper.Initialize();

            logger = LogManager.GetLogger("default");
            logger.Debug("Merhaba");

            /*
<appender name="RollingLogFileAppender" type="log4net.Appender.RollingFileAppender">
    <lockingModel type="log4net.Appender.FileAppender+MinimalLock"/>
    <encoding value="utf-8" />
    <file value="logs/webapi" />
    <datePattern value="_yyyyMMdd.'log'"/>
    <staticLogFileName value="false"/>
    <appendToFile value="true"/>
    <rollingStyle value="Date"/>
    <maxSizeRollBackups value="100"/>
    <maximumFileSize value="15MB"/>
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%-5p%d{ yyyy-MM-dd HH:mm:ss} [%thread] %m %exception %n%n"/>
    </layout>
  </appender>



<log4net debug="true">
  <root>
    <level value="ALL" />
    <appender-ref ref="ConsoleAppender" />
    <appender-ref ref="DebugAppender" />
  </root>
  <logger name="performans" additivity="false">
    <level value="ALL" />
    <appender-ref ref="ConsoleAppender2" />
    <appender-ref ref="DebugAppender2" />
  </logger>
  <appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %message%newline" />
    </layout>
  </appender>
  <appender name="DebugAppender" type="log4net.Appender.DebugAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %message%newline" />
    </layout>
  </appender>
  <appender name="ConsoleAppender2" type="log4net.Appender.ConsoleAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %message%newline" />
    </layout>
  </appender>
  <appender name="DebugAppender2" type="log4net.Appender.DebugAppender">
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date %level %message%newline" />
    </layout>
  </appender>
</log4net>
            */

        }
    }
}
