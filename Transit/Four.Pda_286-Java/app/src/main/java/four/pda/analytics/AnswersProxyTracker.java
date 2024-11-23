package four.pda.analytics;

import android.content.Context;

import com.crashlytics.android.answers.Answers;
import com.crashlytics.android.answers.CustomEvent;
import com.crashlytics.android.answers.SearchEvent;

import java.util.Map;

/**
 * Трекер-прокси, переправляющий запросы в реальный трекер аналитики.
 *
 * @author Pavel Savinov
 * @since 1.0.0
 */
class AnswersProxyTracker extends AnalyticsTracker {

	public AnswersProxyTracker(Context context) {
	}

	@Override
	public void sendCustomEvent(String eventName, Map<String, String> params) {
		CustomEvent event = new CustomEvent(eventName);
		if (params != null) {
			for (String key : params.keySet()) {
				event.putCustomAttribute(key, params.get(key));
			}
		}
		Answers.getInstance().logCustom(event);
	}

	@Override
	public void search(String searchCriteria) {
		Answers.getInstance()
				.logSearch(new SearchEvent()
						.putQuery(searchCriteria));
	}

}
