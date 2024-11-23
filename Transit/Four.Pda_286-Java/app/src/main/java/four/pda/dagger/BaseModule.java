package four.pda.dagger;

import android.content.Context;

import dagger.Module;
import dagger.Provides;
import four.pda.Auth;
import four.pda.EventBus;
import four.pda.EventBus_;
import four.pda.Preferences_;
import four.pda.ui.Keyboard;

/**
 * Created by pavel on 21/05/16.
 */
@Module
public abstract class BaseModule {

	protected Context context;

	public BaseModule(Context context) {
		this.context = context;
	}

	@Provides
	public Keyboard keyboard() {
		return new Keyboard(context);
	}

	@Provides
	public Preferences_ preferences() {
		return new Preferences_(context);
	}

	@Provides
	public EventBus eventBus() {
		return EventBus_.getInstance_(context);
	}

	@Provides
	public Auth auth(Preferences_ preferences, EventBus eventBus) {
		return new Auth(preferences, eventBus);
	}

}
