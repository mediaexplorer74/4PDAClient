using Android.Content;
using Android.Database;
using Android.View;
using Four.Pda;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.List
{
    /// <summary>
    /// Created by asavinova on 10/04/15.
    /// </summary>
    public class ArticlesAdapter : CursorRecyclerViewAdapter<ArticleViewHolder>
    {
        private LayoutInflater inflater;
        public ArticlesAdapter(Context context, Cursor cursor) : base(context, cursor)
        {
            inflater = LayoutInflater.From(context);
        }

        public override ArticleViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = inflater.Inflate(R.layout.article_list_item, parent, false);
            return new ArticleViewHolder(view);
        }

        public override void OnBindViewHolder(ArticleViewHolder viewHolder, Cursor cursor)
        {
            viewHolder.SetCursor(cursor);
        }
    }
}