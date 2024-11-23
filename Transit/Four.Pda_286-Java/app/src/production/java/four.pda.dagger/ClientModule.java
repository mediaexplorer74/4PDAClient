package four.pda.dagger;

import android.content.Context;

import com.franmontiel.persistentcookiejar.PersistentCookieJar;

import dagger.Module;
import dagger.Provides;
import four.pda.client.FourPdaClient;
import okhttp3.OkHttpClient;

/**
 * Created by asavinova on 23/02/16.
 */
@Module
public class ClientModule extends BaseModule {

	public ClientModule(Context context) {
		super(context);
	}

	@Provides
	public PersistentCookieJar cookieJar() {
		return new ForcePersistCookieJar(context);
	}

	@Provides
	public FourPdaClient client(PersistentCookieJar cookieJar) {
		OkHttpClient httpClient = new OkHttpClient.Builder()
				.cookieJar(cookieJar)
				.build();
		return new FourPdaClient(httpClient);
	}

}
