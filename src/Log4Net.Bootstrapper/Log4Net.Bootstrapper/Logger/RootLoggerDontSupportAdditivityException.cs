using System;
using System.Runtime.Serialization;

namespace Log4Net.Bootstrapper.Logger
{
    [Serializable]
    internal class RootLoggerDontSupportAdditivityException : Exception
    {
        public RootLoggerDontSupportAdditivityException()
        {
        }

        public RootLoggerDontSupportAdditivityException(string message) : base(message)
        {
        }

        public RootLoggerDontSupportAdditivityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RootLoggerDontSupportAdditivityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}