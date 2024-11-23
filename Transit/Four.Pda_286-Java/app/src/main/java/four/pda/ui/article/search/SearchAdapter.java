package four.pda.ui.article.search;

import android.content.Context;
import android.database.Cursor;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.recyclerview.widget.RecyclerView;

import four.pda.R;
import four.pda.ui.CursorRecyclerViewAdapter;

/**
 * Created by asavinova on 07/05/16.
 */
public class SearchAdapter extends CursorRecyclerViewAdapter<RecyclerView.ViewHolder> {

	private LayoutInflater inflater;

	public SearchAdapter(Context context, Cursor cursor) {
		super(context, cursor);
		inflater = LayoutInflater.from(context);
	}

	@Override
	public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
		View view = inflater.inflate(R.layout.search_list_item, parent, false);
		return new ItemViewHolder(view);
	}

	@Override
	public void onBindViewHolder(RecyclerView.ViewHolder viewHolder, Cursor cursor) {
		((ItemViewHolder) viewHolder).setCursor(cursor);
	}

	public boolean isEmpty() {
		return getItemCount() == 0;
	}

}
