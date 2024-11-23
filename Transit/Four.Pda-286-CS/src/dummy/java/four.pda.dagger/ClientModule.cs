using Android.Content;
using Com.Franmontiel.Persistentcookiejar;
using Com.Franmontiel.Persistentcookiejar.Cache;
using Com.Franmontiel.Persistentcookiejar.Persistence;
using Dagger;
using Four.Pda.Client;
using Okhttp3;
using System.Diagnostics;

namespace Four.Pda.Dagger
{
    /// <summary>
    /// Created by asavinova on 23/02/16.
    /// </summary>
    public class ClientModule : BaseModule
    {
        public ClientModule(Context context) : base(context)
        {
        }

        public virtual PersistentCookieJar CookieJar()
        {
            return new PersistentCookieJar(new SetCookieCache(), new SharedPrefsCookiePersistor(context));
        }

        public virtual FourPdaClient Client(PersistentCookieJar cookieJar)
        {
            OkHttpClient httpClient = new Builder().CookieJar(cookieJar).Build();
            return new DummyFourPdaClient(httpClient);
        }
    }
}