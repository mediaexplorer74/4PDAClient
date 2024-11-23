using Android.Content;
using Com.Crashlytics.Android.Answers;
using Java.Util;
using System.Diagnostics;

namespace Four.Pda.Analytics
{
    class AnswersProxyTracker : AnalyticsTracker
    {
        public AnswersProxyTracker(Context context)
        {
        }

        public override void SendCustomEvent(string eventName, Dictionary<string, string> @params)
        {
            CustomEvent event = new CustomEvent(eventName);
            if (@params != null)
            {
                foreach (string key in @params.KeySet())
                {
                    @event.PutCustomAttribute(key, @params[key]);
                }
            }

            Answers.GetInstance().LogCustom(@event);
        }

        public override void Search(string searchCriteria)
        {
            Answers.GetInstance().LogSearch(new SearchEvent().PutQuery(searchCriteria));
        }
    }
}