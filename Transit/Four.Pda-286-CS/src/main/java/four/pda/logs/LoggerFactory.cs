using Org.Slf4j;
using Org.Slf4j.Helpers;
using System.Diagnostics;

namespace Four.Pda.Logs
{
    /// <summary>
    /// Created by asavinova on 21/10/15.
    /// </summary>
    public class LoggerFactory : SubstituteLoggerFactory
    {
        public override Logger GetLogger(string name)
        {
            SubstituteLogger logger = ((SubstituteLogger)base.GetLogger(name));
            logger.SetDelegate(new PdaLogger(name));
            return logger;
        }
    }
}