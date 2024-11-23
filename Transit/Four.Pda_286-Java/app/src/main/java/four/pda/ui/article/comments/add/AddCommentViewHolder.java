package four.pda.ui.article.comments.add;

import android.view.View;

import androidx.recyclerview.widget.RecyclerView;

import butterknife.BindView;
import butterknife.ButterKnife;
import four.pda.EventBus_;
import four.pda.R;
import four.pda.analytics.Analytics_;

/**
 * Created by asavinova on 10/03/16.
 */
public class AddCommentViewHolder extends RecyclerView.ViewHolder {

	@BindView(R.id.add_comment_button) View addCommentButton;

	public AddCommentViewHolder(View view) {
		super(view);
		ButterKnife.bind(this, view);

		addCommentButton.setOnClickListener(v -> {
			Analytics_.getInstance_(v.getContext()).comments().add();
			EventBus_.getInstance_(v.getContext())
					.post(new AddCommentEvent());
		});

	}

}
