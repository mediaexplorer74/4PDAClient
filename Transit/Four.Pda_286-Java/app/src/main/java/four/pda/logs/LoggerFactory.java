package four.pda.logs;

import org.slf4j.Logger;
import org.slf4j.helpers.SubstituteLogger;
import org.slf4j.helpers.SubstituteLoggerFactory;

/**
 * Created by asavinova on 21/10/15.
 */
public class LoggerFactory extends SubstituteLoggerFactory {

	@Override
	public Logger getLogger(String name) {
		SubstituteLogger logger = ((SubstituteLogger) super.getLogger(name));
		logger.setDelegate(new PdaLogger(name));
		return logger;
	}

}
