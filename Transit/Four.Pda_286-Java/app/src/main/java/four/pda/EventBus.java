package four.pda;

import org.androidannotations.annotations.EBean;

/**
 * Created by asavinova on 13/04/15.
 */
@EBean(scope = EBean.Scope.Singleton)
public class EventBus extends de.greenrobot.event.EventBus {
}
