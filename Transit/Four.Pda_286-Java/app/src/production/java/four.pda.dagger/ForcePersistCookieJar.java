package four.pda.dagger;

import android.content.Context;

import com.franmontiel.persistentcookiejar.PersistentCookieJar;
import com.franmontiel.persistentcookiejar.cache.SetCookieCache;
import com.franmontiel.persistentcookiejar.persistence.SharedPrefsCookiePersistor;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.List;

import okhttp3.Cookie;
import okhttp3.HttpUrl;

/**
 * Cookie store that force to save all incoming cookies.
 */
class ForcePersistCookieJar extends PersistentCookieJar {

	public ForcePersistCookieJar(Context context) {
        super(new SetCookieCache(), new SharedPrefsCookiePersistor(context));
    }

    @Override
	public synchronized void saveFromResponse(HttpUrl url, List<Cookie> cookies) {

		List<Cookie> updatedCookies = new ArrayList<>();

		for (Cookie cookie : cookies) {

			if (cookie.persistent()) {
				// Don't touch already persistent cookies
				updatedCookies.add(cookie);
				continue;
			}

			// Build new cookie from original

			Cookie.Builder builder = new Cookie.Builder()
                    .path(cookie.path())
                    .domain(cookie.domain())
                    .name(cookie.name())
                    .value(cookie.value());

			Calendar calendar = new GregorianCalendar();
			calendar.setTime(new Date());
			calendar.add(Calendar.YEAR, 1);
			builder.expiresAt(calendar.getTimeInMillis());

			if (cookie.secure()) {
                builder.secure();
            }

			if (cookie.httpOnly()) {
                builder.httpOnly();
            }

			if (cookie.hostOnly()) {
                builder.hostOnlyDomain(cookie.domain());
            }

			updatedCookies.add(builder.build());

		}

		super.saveFromResponse(url, updatedCookies);
	}

}
