package four.pda.logs;

import android.text.TextUtils;
import android.util.Log;

import com.crashlytics.android.Crashlytics;

import org.slf4j.Logger;
import org.slf4j.Marker;
import org.slf4j.helpers.MessageFormatter;

/**
 * Created by asavinova on 21/10/15.
 */
public class PdaLogger implements Logger {

	private static final String TAG = "4PDA";

	private int level = Log.VERBOSE;
	private final String name;

	public PdaLogger(String name) {
		this.name = name;
	}

	public void setLevel(int level) {
		this.level = level;
	}

	@Override
	public String getName() {
		return name;
	}

	@Override
	public boolean isTraceEnabled() {
		return level <= Log.VERBOSE;
	}

	@Override
	public void trace(String msg) {
		log(Log.VERBOSE, msg);
	}

	@Override
	public void trace(String format, Object arg) {
		log(Log.VERBOSE, format, arg);
	}

	@Override
	public void trace(String format, Object arg1, Object arg2) {
		log(Log.VERBOSE, format, arg1, arg2);
	}

	@Override
	public void trace(String format, Object... arguments) {
		log(Log.VERBOSE, format, arguments);
	}

	@Override
	public void trace(String msg, Throwable t) {
		log(Log.VERBOSE, msg, t);
	}

	@Override
	public boolean isTraceEnabled(Marker marker) {
		return isTraceEnabled();
	}

	@Override
	public void trace(Marker marker, String msg) {
		log(Log.VERBOSE, msg);
	}

	@Override
	public void trace(Marker marker, String format, Object arg) {
		log(Log.VERBOSE, format, arg);
	}

	@Override
	public void trace(Marker marker, String format, Object arg1, Object arg2) {
		log(Log.VERBOSE, format, arg1, arg2);
	}

	@Override
	public void trace(Marker marker, String format, Object... arguments) {
		log(Log.VERBOSE, format, arguments);
	}

	@Override
	public void trace(Marker marker, String msg, Throwable t) {
		log(Log.VERBOSE, msg, t);
	}

	@Override
	public boolean isDebugEnabled() {
		return level <= Log.DEBUG;
	}

	@Override
	public void debug(String msg) {
		log(Log.DEBUG, msg);
	}

	@Override
	public void debug(String format, Object arg) {
		log(Log.DEBUG, format, arg);
	}

	@Override
	public void debug(String format, Object arg1, Object arg2) {
		log(Log.DEBUG, format, arg1, arg2);
	}

	@Override
	public void debug(String format, Object... arguments) {
		log(Log.DEBUG, format, arguments);
	}

	@Override
	public void debug(String msg, Throwable t) {
		log(Log.DEBUG, msg, t);
	}

	@Override
	public boolean isDebugEnabled(Marker marker) {
		return isDebugEnabled();
	}

	@Override
	public void debug(Marker marker, String msg) {
		log(Log.DEBUG, msg);
	}

	@Override
	public void debug(Marker marker, String format, Object arg) {
		log(Log.DEBUG, format, arg);
	}

	@Override
	public void debug(Marker marker, String format, Object arg1, Object arg2) {
		log(Log.DEBUG, format, arg1, arg2);
	}

	@Override
	public void debug(Marker marker, String format, Object... arguments) {
		log(Log.DEBUG, format, arguments);
	}

	@Override
	public void debug(Marker marker, String msg, Throwable t) {
		log(Log.DEBUG, msg, t);
	}

	@Override
	public boolean isInfoEnabled() {
		return level <= Log.INFO;
	}

	@Override
	public void info(String msg) {
		log(Log.INFO, msg);
	}

	@Override
	public void info(String format, Object arg) {
		log(Log.INFO, format, arg);
	}

	@Override
	public void info(String format, Object arg1, Object arg2) {
		log(Log.INFO, format, arg1, arg2);
	}

	@Override
	public void info(String format, Object... arguments) {
		log(Log.INFO, format, arguments);
	}

	@Override
	public void info(String msg, Throwable t) {
		log(Log.INFO, msg, t);
	}

	@Override
	public boolean isInfoEnabled(Marker marker) {
		return isInfoEnabled();
	}

	@Override
	public void info(Marker marker, String msg) {
		log(Log.INFO, msg);
	}

	@Override
	public void info(Marker marker, String format, Object arg) {
		log(Log.INFO, format, arg);
	}

	@Override
	public void info(Marker marker, String format, Object arg1, Object arg2) {
		log(Log.INFO, format, arg1, arg2);
	}

	@Override
	public void info(Marker marker, String format, Object... arguments) {
		log(Log.INFO, format, arguments);
	}

	@Override
	public void info(Marker marker, String msg, Throwable t) {
		log(Log.INFO, msg, t);
	}

	@Override
	public boolean isWarnEnabled() {
		return level <= Log.WARN;
	}

	@Override
	public void warn(String msg) {
		log(Log.WARN, msg);
	}

	@Override
	public void warn(String format, Object arg) {
		log(Log.WARN, format, arg);
	}

	@Override
	public void warn(String format, Object arg1, Object arg2) {
		log(Log.WARN, format, arg1, arg2);
	}

	@Override
	public void warn(String format, Object... arguments) {
		log(Log.WARN, format, arguments);
	}

	@Override
	public void warn(String msg, Throwable t) {
		log(Log.WARN, msg, t);
	}

	@Override
	public boolean isWarnEnabled(Marker marker) {
		return isWarnEnabled();
	}

	@Override
	public void warn(Marker marker, String msg) {
		log(Log.WARN, msg);
	}

	@Override
	public void warn(Marker marker, String format, Object arg) {
		log(Log.WARN, format, arg);
	}

	@Override
	public void warn(Marker marker, String format, Object arg1, Object arg2) {
		log(Log.WARN, format, arg1, arg2);
	}

	@Override
	public void warn(Marker marker, String format, Object... arguments) {
		log(Log.WARN, format, arguments);
	}

	@Override
	public void warn(Marker marker, String msg, Throwable t) {
		log(Log.WARN, msg, t);
	}

	@Override
	public boolean isErrorEnabled() {
		return level <= Log.ERROR;
	}

	@Override
	public void error(String msg) {
		log(Log.ERROR, msg);
	}

	@Override
	public void error(String format, Object arg) {
		log(Log.ERROR, format, arg);
	}

	@Override
	public void error(String format, Object arg1, Object arg2) {
		log(Log.ERROR, format, arg1, arg2);
	}

	@Override
	public void error(String format, Object... arguments) {
		log(Log.ERROR, format, arguments);
	}

	@Override
	public void error(String msg, Throwable t) {
		log(Log.ERROR, msg, t);
	}

	@Override
	public boolean isErrorEnabled(Marker marker) {
		return isErrorEnabled();
	}

	@Override
	public void error(Marker marker, String msg) {
		log(Log.ERROR, msg);
	}

	@Override
	public void error(Marker marker, String format, Object arg) {
		log(Log.ERROR, format, arg);
	}

	@Override
	public void error(Marker marker, String format, Object arg1, Object arg2) {
		log(Log.ERROR, format, arg1, arg2);
	}

	@Override
	public void error(Marker marker, String format, Object... arguments) {
		log(Log.ERROR, format, arguments);
	}

	@Override
	public void error(Marker marker, String msg, Throwable t) {
		log(Log.ERROR, msg, t);
	}

	private void log(int priority, String message) {
		log(priority, message, (Throwable) null);
	}

	private void log(int priority, String format, Object... args) {
		String message = MessageFormatter.arrayFormat(format, args).getMessage();
		log(priority, message, (Throwable) null);
	}

	private void log(int priority, String message, Throwable t) {

		StringBuilder messageBuilder = new StringBuilder();

		messageBuilder.append(name).append(':').append(' ');

		if (!TextUtils.isEmpty(message)) {
			messageBuilder.append(message);
		}

		if (t != null) {
			String stackTraceString = Log.getStackTraceString(t);
			messageBuilder.append('\n').append(stackTraceString);
		}

		String msg = messageBuilder.toString();
		Log.println(priority, TAG, msg);
		Crashlytics.log(priority, TAG, msg);
	}

}
