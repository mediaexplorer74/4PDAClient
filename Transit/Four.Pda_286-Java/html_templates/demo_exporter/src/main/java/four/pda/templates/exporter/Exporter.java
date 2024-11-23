package four.pda.templates.exporter;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.IOUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.File;
import java.net.URI;

/**
 * Created by asavinova on 28/07/16.
 */
public abstract class Exporter {

	private static final Logger L = LoggerFactory.getLogger(Exporter.class);

	private static final String SERVER = "http://4pda.ru/";

	protected void convertPage(String url, String filename) {
		try {
			String originalPage = IOUtils.toString(URI.create(SERVER + url), "CP1251");
			String croppedPage = getCroppedPage(originalPage);
			String wrappedPage = getWrappedPage(croppedPage);
			FileUtils.write(new File(filename + ".htm"), wrappedPage);
		} catch (Exception e) {
			L.error("Can't convert page " + filename, e);
		}
	}

	protected abstract String getCroppedPage(String originalPage);

	protected abstract String getWrappedPage(String croppedPage);

}
