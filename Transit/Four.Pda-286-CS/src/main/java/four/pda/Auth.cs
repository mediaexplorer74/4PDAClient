using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by asavinova on 22/06/16.
    /// </summary>
    public class Auth
    {
        private readonly Preferences_ preferences;
        private readonly EventBus eventBus;
        public Auth(Preferences_ preferences, EventBus eventBus)
        {
            this.preferences = preferences;
            this.eventBus = eventBus;
        }

        public virtual bool IsAuthorized()
        {
            return preferences.ProfileId().Get() > 0;
        }

        public virtual void Login(long memberId)
        {
            preferences.ProfileId().Put(memberId);
        }

        public virtual void Logout()
        {
            preferences.ProfileId().Put(0);
            preferences.ProfileLogin().Put(null);
            preferences.ProfilePhoto().Put(null);
            eventBus.Post(new UpdateProfileEvent());
        }

        public virtual void SetProfile(Profile profile)
        {
            preferences.ProfileLogin().Put(profile.GetLogin());
            preferences.ProfilePhoto().Put(profile.GetPhoto());
            eventBus.Post(new UpdateProfileEvent());
        }

        public virtual Profile GetProfile()
        {
            Profile profile = new Profile();
            profile.SetLogin(preferences.ProfileLogin().Get());
            profile.SetPhoto(preferences.ProfilePhoto().Get());
            return profile;
        }

        public virtual long GetProfileId()
        {
            return preferences.ProfileId().Get();
        }
    }
}