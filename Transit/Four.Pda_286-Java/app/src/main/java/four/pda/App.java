package four.pda;

import android.app.Application;

import com.crashlytics.android.Crashlytics;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import four.pda.dagger.ClientModule;
import four.pda.dagger.DaggerFourPdaComponent;
import four.pda.dagger.FourPdaComponent;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.SilentLogger;

/**
 * Application.
 *
 * @author Anna Savinova.
 * @author Pavel Savinov.
 */
public class App extends Application {

	private static final Logger L = LoggerFactory.getLogger(App.class);

	private FourPdaComponent component;

	@Override
	public void onCreate() {
		super.onCreate();
		tuneLogs();
		L.debug("Start application");

		component = DaggerFourPdaComponent.builder()
				.clientModule(new ClientModule(this))
				.build();
	}

	private void tuneLogs() {
		Fabric fabric = new Fabric.Builder(this)
				.kits(new Crashlytics())
				.logger(new SilentLogger())
				.build();
		Fabric.with(fabric);
	}

	public FourPdaComponent component() {
		return component;
	}

}
