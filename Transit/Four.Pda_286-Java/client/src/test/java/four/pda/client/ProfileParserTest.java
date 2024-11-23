package four.pda.client;

import org.junit.Assert;
import org.junit.Test;

import java.io.IOException;

import four.pda.client.model.Profile;
import four.pda.client.parsers.ProfileParser;

/**
 * Created by asavinova on 07/06/16.
 */
public class ProfileParserTest extends AbstractTest {

	@Test
	public void checkVarannUserProfileFields() throws IOException {
		String pageSource = getHtmlSource("/forum/index.php?showuser=4975039");
		Profile profile = new ProfileParser().parse(pageSource);

		Assert.assertEquals("Unexpected login name", "var.ann", profile.getLogin());
		Assert.assertFalse("Avatar is empty", profile.getPhoto().isEmpty());
	}

	@Test
	public void checkWadym72UserProfileFields() throws IOException {
		String pageSource = getHtmlSource("/forum/index.php?showuser=1929816");
		Profile profile = new ProfileParser().parse(pageSource);

		Assert.assertEquals("Unexpected login name", "Wadym72", profile.getLogin());
		Assert.assertFalse("Avatar is empty", profile.getPhoto().isEmpty());
	}

	@Test
	public void checkNewsUserProfileFields() throws IOException {
		String pageSource = getHtmlSource("/forum/index.php?showuser=204809");
		Profile profile = new ProfileParser().parse(pageSource);

		Assert.assertEquals("Unexpected login name", "News", profile.getLogin());
		Assert.assertFalse("Avatar is empty", profile.getPhoto().isEmpty());
	}

}
