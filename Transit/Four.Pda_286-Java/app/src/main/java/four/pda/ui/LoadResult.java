package four.pda.ui;

import com.crashlytics.android.Crashlytics;

import four.pda.client.exceptions.ParseException;

/**
 * Created by asavinova on 24/01/16.
 */
public class LoadResult<T> {

	private T data;
	private Exception exception;

	public LoadResult(T data) {
		this.data = data;
	}

	public LoadResult(Exception exception) {
		this.exception = exception;
		if (exception instanceof ParseException) {
			Crashlytics.logException(exception);
		}
	}

	public boolean isSuccess() {
		return exception == null;
	}

	public boolean isError() {
		return exception != null;
	}

	public T getData() {
		return data;
	}

	public Exception getException() {
		return exception;
	}

}
