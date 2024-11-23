package four.pda.ui.article.search;

import android.database.Cursor;
import android.text.Html;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Date;

import butterknife.BindView;
import butterknife.ButterKnife;
import four.pda.DateFormats;
import four.pda.EventBus;
import four.pda.EventBus_;
import four.pda.R;
import four.pda.dao.SearchArticleDao;
import four.pda.ui.Images;
import four.pda.ui.article.LabelView;
import four.pda.ui.article.ShowArticleEvent;
import four.pda.ui.profile.ProfileActivity_;

/**
 * Created by asavinova on 08/05/16.
 */
public class ItemViewHolder extends RecyclerView.ViewHolder {

	private static final Logger L = LoggerFactory.getLogger(ItemViewHolder.class);

	@BindView(R.id.image_view) ImageView imageView;
	@BindView(R.id.title_view) TextView titleView;
	@BindView(R.id.date_view) TextView dateView;
	@BindView(R.id.author_view) TextView authorView;
	@BindView(R.id.label_view) LabelView labelView;

	private final TextView descriptionView;

	private long id;
	private Date date;
	private String title;
	private String image;
	private long authorId;
	private String authorName;
	private String labelName;
	private String labelColor;

	private final EventBus eventBus;

	public ItemViewHolder(View view) {
		super(view);
		ButterKnife.bind(this, view);

		eventBus = EventBus_.getInstance_(view.getContext());

		itemView.setOnClickListener(v -> {
			ShowArticleEvent event = new ShowArticleEvent(
					id, date, title, image,
					authorId, authorName,
					labelName, labelColor
			);
			eventBus.post(event);
		});

		descriptionView = (TextView) itemView.findViewById(R.id.description_view);

		if (descriptionView != null) {
			descriptionView.addOnLayoutChangeListener(new MaxLinesListener());
		}

		authorView.setOnClickListener(v -> {
			if (authorId > 0) {
				ProfileActivity_.intent(v.getContext())
						.profileId(authorId)
						.start();
			}
		});

	}

	public void setCursor(Cursor cursor) {

		id = cursor.getLong(SearchArticleDao.Properties.Id.ordinal);

		title = cursor.getString(SearchArticleDao.Properties.Title.ordinal);
		titleView.setText(title);

		image = cursor.getString(SearchArticleDao.Properties.Image.ordinal);
		Images.load(imageView, image);

		date = new Date(cursor.getLong(SearchArticleDao.Properties.Date.ordinal));
		String verboseDate = DateFormats.VERBOSE.format(date);
		dateView.setText(verboseDate);

		authorId = cursor.getLong(SearchArticleDao.Properties.AuthorId.ordinal);
		authorName = cursor.getString(SearchArticleDao.Properties.AuthorName.ordinal);
		authorView.setText(authorName);

		labelName = cursor.getString(SearchArticleDao.Properties.LabelName.ordinal);
		labelColor = cursor.getString(SearchArticleDao.Properties.LabelColor.ordinal);
		labelView.setLabel(labelName, labelColor);

		if (descriptionView != null) {
			String description = cursor.getString(SearchArticleDao.Properties.Description.ordinal);
			descriptionView.setText(Html.fromHtml(description));
		}

	}

	private static class MaxLinesListener implements View.OnLayoutChangeListener {

		@Override
        public void onLayoutChange(View v, int left, int top, int right, int bottom,
								   int oldLeft, int oldTop, int oldRight, int oldBottom) {

            final TextView view = ((TextView) v);

            int viewHeight = view.getMeasuredHeight();
            int lineHeight = view.getLineHeight();

            int maxLines = (int) (viewHeight / ((double) lineHeight));

            if (view.getMaxLines() != maxLines) {

                view.setMaxLines(maxLines);
                view.postDelayed(view::requestLayout, 100);

            }

        }
	}
}
