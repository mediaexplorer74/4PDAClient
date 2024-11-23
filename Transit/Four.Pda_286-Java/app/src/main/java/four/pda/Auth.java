package four.pda;

import four.pda.client.model.Profile;
import four.pda.ui.UpdateProfileEvent;

/**
 * Created by asavinova on 22/06/16.
 */
public class Auth {

	private final Preferences_ preferences;
	private final EventBus eventBus;

	public Auth(Preferences_ preferences, EventBus eventBus) {
		this.preferences = preferences;
		this.eventBus = eventBus;
	}

	public boolean isAuthorized() {
		return preferences.profileId().get() > 0;
	}

	public void login(Long memberId) {
		preferences.profileId().put(memberId);
	}

	public void logout() {
		preferences.profileId().put(0l);
		preferences.profileLogin().put(null);
		preferences.profilePhoto().put(null);

		eventBus.post(new UpdateProfileEvent());
	}

	public void setProfile(Profile profile) {
		preferences.profileLogin().put(profile.getLogin());
		preferences.profilePhoto().put(profile.getPhoto());

		eventBus.post(new UpdateProfileEvent());
	}

	public Profile getProfile() {
		Profile profile = new Profile();
		profile.setLogin(preferences.profileLogin().get());
		profile.setPhoto(preferences.profilePhoto().get());
		return profile;
	}

	public Long getProfileId() {
		return preferences.profileId().get();
	}

}
