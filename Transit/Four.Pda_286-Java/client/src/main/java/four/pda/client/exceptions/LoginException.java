package four.pda.client.exceptions;

import java.util.List;

/**
 * Created by asavinova on 22/02/16.
 */
public class LoginException extends RuntimeException {

	private List<String> errors;

	public LoginException(List<String> errors) {
		this.errors = errors;
	}

	public List<String> getErrors() {
		return errors;
	}

}
