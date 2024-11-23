using Org.Slf4j;
using Org.Slf4j.Spi;
using Four.Pda.Logs;
using System.Diagnostics;

namespace Org.Slf4j.Impl
{
    /// <summary>
    /// Created by asavinova on 21/10/15.
    /// </summary>
    public class StaticLoggerBinder : LoggerFactoryBinder
    {
        public static readonly StaticLoggerBinder SINGLETON = new StaticLoggerBinder();
        public static StaticLoggerBinder GetSingleton()
        {
            return SINGLETON;
        }

        public virtual ILoggerFactory GetLoggerFactory()
        {
            return new LoggerFactory();
        }

        public virtual string GetLoggerFactoryClassStr()
        {
            return typeof(LoggerFactory).GetName();
        }
    }
}