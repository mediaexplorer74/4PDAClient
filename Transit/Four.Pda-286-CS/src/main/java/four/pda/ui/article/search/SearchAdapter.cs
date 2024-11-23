using Android.Content;
using Android.Database;
using Android.View;
using Androidx.Recyclerview.Widget;
using Four.Pda;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Search
{
    /// <summary>
    /// Created by asavinova on 07/05/16.
    /// </summary>
    public class SearchAdapter : CursorRecyclerViewAdapter<RecyclerView.ViewHolder>
    {
        private LayoutInflater inflater;
        public SearchAdapter(Context context, Cursor cursor) : base(context, cursor)
        {
            inflater = LayoutInflater.From(context);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = inflater.Inflate(R.layout.search_list_item, parent, false);
            return new ItemViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, Cursor cursor)
        {
            ((ItemViewHolder)viewHolder).SetCursor(cursor);
        }

        public virtual bool IsEmpty()
        {
            return GetItemCount() == 0;
        }
    }
}