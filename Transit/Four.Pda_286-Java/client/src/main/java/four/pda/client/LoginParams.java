package four.pda.client;

/**
 * Created by asavinova on 21/02/16.
 */
public class LoginParams {

	private String login;
	private String password;
	private String captchaTime;
	private String captchaSig;
	private String captcha;

	public String getLogin() {
		return login;
	}

	public void setLogin(String login) {
		this.login = login;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getCaptchaTime() {
		return captchaTime;
	}

	public void setCaptchaTime(String captchaTime) {
		this.captchaTime = captchaTime;
	}

	public String getCaptchaSig() {
		return captchaSig;
	}

	public void setCaptchaSig(String captchaSig) {
		this.captchaSig = captchaSig;
	}

	public String getCaptcha() {
		return captcha;
	}

	public void setCaptcha(String captcha) {
		this.captcha = captcha;
	}

}
