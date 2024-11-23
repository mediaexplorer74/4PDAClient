package four.pda.client;

import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;

import four.pda.client.model.Captcha;
import four.pda.client.parsers.CaptchaParser;

/**
 * Created by asavinova on 08/02/16.
 */
public class LoginTest extends AbstractTest {

	@Test
	public void captcha() throws IOException {
		String pageSource = getHtmlSource("/forum/index.php?act=login");
		Captcha captcha = new CaptchaParser().parse(pageSource);

		System.out.println("Captcha time " + captcha.getTime());
		System.out.println("Captcha sig " + captcha.getSig());
		System.out.println("Captcha url " + captcha.getUrl());

		Assert.assertFalse("Wrong captcha time", captcha.getTime().isEmpty());
		Assert.assertFalse("Wrong captcha sig", captcha.getSig().isEmpty());
		Assert.assertFalse("Wrong captcha url", captcha.getUrl().isEmpty());
	}

}
