package four.pda.templates.exporter;

import four.pda.client.parsers.ProfileParser;
import four.pda.template.ProfileTemplate;

/**
 * Created by asavinova on 26/07/16.
 */
public class ProfileExporter extends Exporter {

	public static void main(String[] args) {
		ProfileExporter exporter = new ProfileExporter();
		exporter.convertPage("forum/index.php?showuser=4975039", "profile_varann");
		exporter.convertPage("forum/index.php?showuser=1929816", "profile_Wadym72");
		exporter.convertPage("forum/index.php?showuser=204809", "profile_News");
		exporter.convertPage("forum/index.php?showuser=1477609", "profile_Syncaine");
	}

	@Override
	protected String getCroppedPage(String originalPage) {
		return new ProfileParser().parse(originalPage).getInfo();
	}

	@Override
	protected String getWrappedPage(String croppedPage) {
		return new ProfileTemplate().make(croppedPage);
	}

}

