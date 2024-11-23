package four.pda.client.testapp;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;
import java.net.CookieManager;
import java.net.CookiePolicy;
import java.util.Scanner;

import four.pda.client.FourPdaClient;
import four.pda.client.LoginParams;
import four.pda.client.exceptions.LoginException;
import four.pda.client.model.Captcha;
import four.pda.client.model.Profile;
import okhttp3.JavaNetCookieJar;
import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;

/**
 * Created by asavinova on 09/02/16.
 */
public class AuthTest {

	private static final Logger L = LoggerFactory.getLogger(AuthTest.class);

	public static void main(String[] args) throws IOException {
		Scanner scanner = new Scanner(System.in);

		CookieManager cookieManager = new CookieManager();
		cookieManager.setCookiePolicy(CookiePolicy.ACCEPT_ALL);

		HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
		logging.setLevel(HttpLoggingInterceptor.Level.HEADERS);

		OkHttpClient httpClient = new OkHttpClient.Builder()
				.cookieJar(new JavaNetCookieJar(cookieManager))
				.addInterceptor(logging)
				.build();

		FourPdaClient client = new FourPdaClient(httpClient);
		LoginParams params = new LoginParams();

		Captcha captcha = client.getCaptcha();
		params.setCaptchaTime(captcha.getTime());
		params.setCaptchaSig(captcha.getSig());
		System.out.println("Captcha url: " + captcha.getUrl());

		System.out.print("Captcha: ");
		String captchaValue = scanner.nextLine();
		params.setCaptcha(captchaValue);

		params.setLogin(args[0]);
		params.setPassword(args[1]);

		try {
			long memberId = client.login(params);
			L.debug("Login result OK");

			Profile profile = client.getProfile(memberId);
			L.debug("Profile login: " + profile.getLogin());
			L.debug("Profile photo: " + profile.getPhoto());
		} catch (LoginException e) {
			L.debug("Login result ERROR");
			for (String error : e.getErrors()) {
				L.debug("Error: " + error);
			}
		}

		boolean isSuccess = client.logout();
		if (isSuccess) {
			L.debug("Logout success");
		} else {
			L.debug("Logout not success");
		}
	}

}
