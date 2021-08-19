using log4net.Appender;
using log4net.Layout;
using System.Net;

namespace Log4Net.Bootstrapper.AppenderBuilders
{
    public class UdpAppenderBuilder : IAppenderBuilder
    {
        private readonly UdpAppender _udpAppender;
        public UdpAppenderBuilder(
            string name, 
            string remoteAddress,
            int remotePort,
            string patternLayoutPattern = "%-5level %logger [%property{NDC}] - %message%newline")
        {
            _udpAppender = new UdpAppender
            {
                Name = name,
                RemoteAddress = IPAddress.Parse(remoteAddress),
                RemotePort = remotePort,
            };

            if (!string.IsNullOrWhiteSpace(patternLayoutPattern))
            {
                _udpAppender.Layout = new PatternLayout(patternLayoutPattern);
            }
        }
        public IAppender Appender => _udpAppender;

        public UdpAppenderBuilder Encoding(string encodingName)
        {
            _udpAppender.Encoding = System.Text.Encoding.GetEncoding(encodingName);
            return this;
        }
        public UdpAppenderBuilder Threshold(log4net.Core.Level threshold)
        {
            _udpAppender.Threshold = threshold;
            return this;
        }

        public UdpAppenderBuilder Layout(ILayout layout)
        {
            _udpAppender.Layout = layout;
            return this;
        }


        public UdpAppenderBuilder RemoteAddress(string remoteAddress)
        {
            _udpAppender.RemoteAddress = IPAddress.Parse(remoteAddress);
            return this;
        }
        public UdpAppenderBuilder RemotePort(int remotePort)
        {
            _udpAppender.RemotePort = remotePort;
            return this;
        }
        public UdpAppenderBuilder LocalPort(int localPort)
        {
            _udpAppender.LocalPort = localPort;
            return this;
        }

    }
    /*
<appender name="UdpAppender" type="log4net.Appender.UdpAppender">
    <localPort value="8080" />
    <remoteAddress value="224.0.0.1" />
    <remotePort value="8080" />
    <layout type="log4net.Layout.PatternLayout, log4net">
        <conversionPattern value="%-5level %logger [%property{NDC}] - %message%newline" />
    </layout>
</appender>
     
     */
}