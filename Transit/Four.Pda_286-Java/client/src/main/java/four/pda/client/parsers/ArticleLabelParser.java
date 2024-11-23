package four.pda.client.parsers;

import org.apache.commons.lang3.StringUtils;
import org.jsoup.nodes.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Set;

import four.pda.client.exceptions.ParseException;
import four.pda.client.model.AbstractArticle;

/**
 * @author Pavel Savinov (swapii@gmail.com)
 */
public class ArticleLabelParser {

	private static final Logger L = LoggerFactory.getLogger(ArticleLabelParser.class);

	public AbstractArticle.Label parse(Element labelEl) {

		if (labelEl == null) {
			return null;
		}

		AbstractArticle.Label label = new AbstractArticle.Label();
		String name = labelEl.text().trim();

		// Replace '&nbsp;' symbol
		name = StringUtils.strip(name, "\u00A0");

		label.setName(name);
		label.setColor(getLabelColor(labelEl));
		return label;
	}

	private String getLabelColor(Element labelEl) {

		Set<String> classNames = labelEl.classNames();

		// We don't need 'label' class, only color
		classNames.remove("label");

		switch (classNames.size()) {
			case 0:
				return null;
			case 1:
				return classNames.iterator().next();
			default:
				throw new ParseException("Unexpected classes count for label element");
		}
	}

}
