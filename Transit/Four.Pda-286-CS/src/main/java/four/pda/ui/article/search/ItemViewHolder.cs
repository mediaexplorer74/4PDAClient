using Android.Database;
using Android.Text;
using Android.View;
using Android.Widget;
using Androidx.Recyclerview.Widget;
using Org.Slf4j;
using Java.Util;
using Butterknife;
using Four.Pda;
using Four.Pda.Dao;
using Four.Pda.Ui;
using Four.Pda.Ui.Article;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Search
{
    /// <summary>
    /// Created by asavinova on 08/05/16.
    /// </summary>
    public class ItemViewHolder : ViewHolder
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ItemViewHolder));
        ImageView imageView;
        TextView titleView;
        TextView dateView;
        TextView authorView;
        LabelView labelView;
        private readonly TextView descriptionView;
        private long id;
        private Date date;
        private string title;
        private string image;
        private long authorId;
        private string authorName;
        private string labelName;
        private string labelColor;
        private readonly EventBus eventBus;
        public ItemViewHolder(View view) : base(view)
        {
            ButterKnife.Bind(this, view);
            eventBus = EventBus_.GetInstance_(view.GetContext());
            itemView.SetOnClickListener((v) =>
            {
                ShowArticleEvent event = new ShowArticleEvent(id, date, title, image, authorId, authorName, labelName, labelColor);
                eventBus.Post(@event);
            });
            descriptionView = (TextView)itemView.FindViewById(R.id.description_view);
            if (descriptionView != null)
            {
                descriptionView.AddOnLayoutChangeListener(new MaxLinesListener());
            }

            authorView.SetOnClickListener((v) =>
            {
                if (authorId > 0)
                {
                    ProfileActivity_.Intent(v.GetContext()).ProfileId(authorId).Start();
                }
            });
        }

        public virtual void SetCursor(Cursor cursor)
        {
            id = cursor.GetLong(SearchArticleDao.Properties.Id.ordinal);
            title = cursor.GetString(SearchArticleDao.Properties.Title.ordinal);
            titleView.SetText(title);
            image = cursor.GetString(SearchArticleDao.Properties.Image.ordinal);
            Images.Load(imageView, image);
            date = new Date(cursor.GetLong(SearchArticleDao.Properties.Date.ordinal));
            string verboseDate = DateFormats.VERBOSE.Format(date);
            dateView.SetText(verboseDate);
            authorId = cursor.GetLong(SearchArticleDao.Properties.AuthorId.ordinal);
            authorName = cursor.GetString(SearchArticleDao.Properties.AuthorName.ordinal);
            authorView.SetText(authorName);
            labelName = cursor.GetString(SearchArticleDao.Properties.LabelName.ordinal);
            labelColor = cursor.GetString(SearchArticleDao.Properties.LabelColor.ordinal);
            labelView.SetLabel(labelName, labelColor);
            if (descriptionView != null)
            {
                string description = cursor.GetString(SearchArticleDao.Properties.Description.ordinal);
                descriptionView.SetText(Html.FromHtml(description));
            }
        }

        private class MaxLinesListener : OnLayoutChangeListener
        {
            public virtual void OnLayoutChange(View v, int left, int top, int right, int bottom, int oldLeft, int oldTop, int oldRight, int oldBottom)
            {
                TextView view = ((TextView)v);
                int viewHeight = view.GetMeasuredHeight();
                int lineHeight = view.GetLineHeight();
                int maxLines = (int)(viewHeight / ((double)lineHeight));
                if (view.GetMaxLines() != maxLines)
                {
                    view.SetMaxLines(maxLines);
                    view.PostDelayed(view.RequestLayout(), 100);
                }
            }
        }
    }
}