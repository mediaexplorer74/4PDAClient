package four.pda.ui.article.list;

import android.database.Cursor;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import java.util.Date;

import butterknife.BindView;
import butterknife.ButterKnife;
import four.pda.DateFormats;
import four.pda.EventBus;
import four.pda.EventBus_;
import four.pda.R;
import four.pda.analytics.Analytics_;
import four.pda.dao.ArticleDao;
import four.pda.ui.Images;
import four.pda.ui.article.LabelView;
import four.pda.ui.article.ShowArticleEvent;
import four.pda.ui.profile.ProfileActivity_;

/**
 * Created by pavel on 12/04/15.
 */
public class ArticleViewHolder extends RecyclerView.ViewHolder {

	@BindView(R.id.image_view) ImageView imageView;
	@BindView(R.id.label_view) LabelView labelView;
	@BindView(R.id.title_view) TextView titleView;
	@BindView(R.id.date_view) TextView dateView;
	@BindView(R.id.author_view) TextView authorView;
	@BindView(R.id.comments_count_text_view) TextView commentsCountView;

	private long id;
	private Date date;
	private String title;
	private String image;
	private long authorId;
	private String authorName;
	private String labelName;
	private String labelColor;

	private final EventBus eventBus;

	public ArticleViewHolder(View view) {
		super(view);
		ButterKnife.bind(this, view);

		eventBus = EventBus_.getInstance_(view.getContext());

		itemView.setOnClickListener(v -> {
			if (id > 0) {
				ShowArticleEvent event = new ShowArticleEvent(
						id, date, title, image,
						authorId, authorName,
						labelName, labelColor
				);
				eventBus.post(event);
			}
		});

		authorView.setOnClickListener(v -> {
			if (authorId > 0) {
				Analytics_.getInstance_(v.getContext())
						.articlesList()
						.profileClicked();
				ProfileActivity_.intent(v.getContext())
						.profileId(authorId)
						.start();
			}
		});
	}

	public void setCursor(Cursor cursor) {

		id = cursor.getLong(ArticleDao.Properties.Id.ordinal);
		authorId = cursor.getLong(ArticleDao.Properties.AuthorId.ordinal);

		date = new Date(cursor.getLong(ArticleDao.Properties.Date.ordinal));
		String verboseDate = DateFormats.VERBOSE.format(date);
		dateView.setText(verboseDate);

		title = cursor.getString(ArticleDao.Properties.Title.ordinal);
		titleView.setText(title);

		image = cursor.getString(ArticleDao.Properties.Image.ordinal);
		Images.load(imageView, image);

		authorName = cursor.getString(ArticleDao.Properties.AuthorName.ordinal);
		authorView.setText(authorName);

		labelName = cursor.getString(ArticleDao.Properties.LabelName.ordinal);
		labelColor = cursor.getString(ArticleDao.Properties.LabelColor.ordinal);
		labelView.setLabel(labelName, labelColor);

		int commentsCount = cursor.getInt(ArticleDao.Properties.CommentsCount.ordinal);
		commentsCountView.setText(String.valueOf(commentsCount));

	}

}
