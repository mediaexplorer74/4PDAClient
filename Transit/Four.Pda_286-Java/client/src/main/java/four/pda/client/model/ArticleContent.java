package four.pda.client.model;

import java.util.List;

/**
 * Created by asavinova on 01/09/16.
 */
public class ArticleContent {

	private String content;
	private List<String> images;
	private int commentsCount;
	private AbstractArticle.Label label;

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public List<String> getImages() {
		return images;
	}

	public void setImages(List<String> images) {
		this.images = images;
	}

	public int getCommentsCount() {
		return commentsCount;
	}

	public void setCommentsCount(int commentsCount) {
		this.commentsCount = commentsCount;
	}

	public AbstractArticle.Label getLabel() {
		return label;
	}

	public void setLabel(AbstractArticle.Label label) {
		this.label = label;
	}

}
