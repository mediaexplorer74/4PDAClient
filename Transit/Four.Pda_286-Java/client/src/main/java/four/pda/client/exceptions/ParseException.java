package four.pda.client.exceptions;

/**
 * Created by asavinova on 22/02/16.
 */
public class ParseException extends RuntimeException {

	public ParseException(String message) {
		super(message);
	}

	public ParseException(String message, Exception e) {
		super(message, e);
	}

}
