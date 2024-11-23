package four.pda.analytics;

import java.util.Map;

/**
 * Трекер-пустышка без логики.
 *
 * @author Pavel Savinov
 * @since 1.0.0
 */
class AnalyticsTracker {

    public void sendCustomEvent(String event) {
		sendCustomEvent(event, null);
    }

    public void sendCustomEvent(String event, Map<String, String> params) {
    }

	public void search(String searchCriteria) {
	}

}
