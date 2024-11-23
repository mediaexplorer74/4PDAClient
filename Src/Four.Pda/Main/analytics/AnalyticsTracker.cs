using Java.Util;
using System.Diagnostics;

namespace Four.Pda.Analytics
{
    class AnalyticsTracker
    {
        public virtual void SendCustomEvent(string @event)
        {
            SendCustomEvent(@event, null);
        }

        public virtual void SendCustomEvent(string @event, Dictionary<string, string> @params)
        {
        }

        public virtual void Search(string searchCriteria)
        {
        }
    }
}