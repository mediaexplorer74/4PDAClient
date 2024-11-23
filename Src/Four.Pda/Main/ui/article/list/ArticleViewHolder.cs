using Android.Database;
using Android.View;
using Android.Widget;
using Androidx.Recyclerview.Widget;
using Java.Util;
using Butterknife;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Dao;
using Four.Pda.Ui;
using Four.Pda.Ui.Article;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.List
{
    /// <summary>
    /// Created by pavel on 12/04/15.
    /// </summary>
    public class ArticleViewHolder : ViewHolder
    {
        ImageView imageView;
        LabelView labelView;
        TextView titleView;
        TextView dateView;
        TextView authorView;
        TextView commentsCountView;
        private long id;
        private Date date;
        private string title;
        private string image;
        private long authorId;
        private string authorName;
        private string labelName;
        private string labelColor;
        private readonly EventBus eventBus;
        public ArticleViewHolder(View view) : base(view)
        {
            ButterKnife.Bind(this, view);
            eventBus = EventBus_.GetInstance_(view.GetContext());
            itemView.SetOnClickListener((v) =>
            {
                if (id > 0)
                {
                    ShowArticleEvent event = new ArticleViewHolder(id, date, title, image, authorId, authorName, labelName, labelColor);
                    eventBus.Post(@event);
                }
            });
            authorView.SetOnClickListener((v) =>
            {
                if (authorId > 0)
                {
                    Analytics_.GetInstance_(v.GetContext()).ArticlesList().ProfileClicked();
                    ProfileActivity_.Intent(v.GetContext()).ProfileId(authorId).Start();
                }
            });
        }

        public virtual void SetCursor(Cursor cursor)
        {
            id = cursor.GetLong(ArticleDao.Properties.Id.ordinal);
            authorId = cursor.GetLong(ArticleDao.Properties.AuthorId.ordinal);
            date = new Date(cursor.GetLong(ArticleDao.Properties.Date.ordinal));
            string verboseDate = DateFormats.VERBOSE.Format(date);
            dateView.SetText(verboseDate);
            title = cursor.GetString(ArticleDao.Properties.Title.ordinal);
            titleView.SetText(title);
            image = cursor.GetString(ArticleDao.Properties.Image.ordinal);
            Images.Load(imageView, image);
            authorName = cursor.GetString(ArticleDao.Properties.AuthorName.ordinal);
            authorView.SetText(authorName);
            labelName = cursor.GetString(ArticleDao.Properties.LabelName.ordinal);
            labelColor = cursor.GetString(ArticleDao.Properties.LabelColor.ordinal);
            labelView.SetLabel(labelName, labelColor);
            int commentsCount = cursor.GetInt(ArticleDao.Properties.CommentsCount.ordinal);
            commentsCountView.SetText(String.ValueOf(commentsCount));
        }
    }
}