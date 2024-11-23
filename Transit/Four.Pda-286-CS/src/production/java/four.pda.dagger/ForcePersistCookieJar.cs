using Android.Content;
using Com.Franmontiel.Persistentcookiejar;
using Com.Franmontiel.Persistentcookiejar.Cache;
using Com.Franmontiel.Persistentcookiejar.Persistence;
using Java.Util;
using Okhttp3;
using System.Diagnostics;

namespace Four.Pda.Dagger
{
    /// <summary>
    /// Cookie store that force to save all incoming cookies.
    /// </summary>
    class ForcePersistCookieJar : PersistentCookieJar
    {
        public ForcePersistCookieJar(Context context) : base(new SetCookieCache(), new SharedPrefsCookiePersistor(context))
        {
        }

        public override void SaveFromResponse(HttpUrl url, IList<Cookie> cookies)
        {
            lock (this)
            {
                IList<Cookie> updatedCookies = new List();
                foreach (Cookie cookie in cookies)
                {
                    if (cookie.Persistent())
                    {

                        // Don't touch already persistent cookies
                        updatedCookies.Add(cookie);
                        continue;
                    }


                    // Build new cookie from original
                    Cookie.Builder builder = new Builder().Path(cookie.Path()).Domain(cookie.Domain()).Name(cookie.Name()).Value(cookie.Value());
                    Calendar calendar = new GregorianCalendar();
                    calendar.SetTime(new Date());
                    calendar.Add(Calendar.YEAR, 1);
                    builder.ExpiresAt(calendar.GetTimeInMillis());
                    if (cookie.Secure())
                    {
                        builder.Secure();
                    }

                    if (cookie.HttpOnly())
                    {
                        builder.HttpOnly();
                    }

                    if (cookie.HostOnly())
                    {
                        builder.HostOnlyDomain(cookie.Domain());
                    }

                    updatedCookies.Add(builder.Build());
                }

                base.SaveFromResponse(url, updatedCookies);
            }
        }
    }
}