package four.pda.ui.article;

import java.util.Date;

/**
 * Created by asavinova on 13/04/15.
 */
public class ShowArticleEvent {

	private long id;
	private Date date;
	private String title;
	private String image;
	private long authorId;
	private String authorName;
	private final String labelName;
	private final String labelColor;

	public ShowArticleEvent(long id, Date date, String title, String image,
							long authorId, String authorName,
							String labelName, String labelColor) {
		this.id = id;
		this.date = date;
		this.title = title;
		this.image = image;
		this.authorId = authorId;
		this.authorName = authorName;
		this.labelName = labelName;
		this.labelColor = labelColor;
	}

	public long getId() {
		return id;
	}

	public Date getDate() {
		return date;
	}

	public String getTitle() {
		return title;
	}

	public String getImage() {
		return image;
	}

	public long getAuthorId() {
		return authorId;
	}

	public String getAuthorName() {
		return authorName;
	}

	public String getLabelName() {
		return labelName;
	}

	public String getLabelColor() {
		return labelColor;
	}

}
