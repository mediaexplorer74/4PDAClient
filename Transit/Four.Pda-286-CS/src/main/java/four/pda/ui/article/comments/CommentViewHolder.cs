using Android.Text;
using Android.View;
using Android.Widget;
using Androidx.Recyclerview.Widget;
using Java.Text;
using Butterknife;
using Four.Pda;
using Four.Pda.Client.Model;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 05/12/15.
    /// </summary>
    class CommentViewHolder : ViewHolder
    {
        private static readonly SimpleDateFormat DATE_FORMAT = new SimpleDateFormat("dd.MM.yy HH:ss");
        View authorInfoView;
        View delimiterView;
        TextView nickView;
        TextView dateView;
        View likesCheckView;
        TextView likesView;
        TextView contentView;
        public CommentViewHolder(View view) : base(view)
        {
            ButterKnife.Bind(this, view);
        }

        public virtual void SetComment(Comment comment)
        {
            nickView.SetText(comment.GetUser().GetNickname());
            string verboseDate = DATE_FORMAT.Format(comment.GetDate());
            dateView.SetText(verboseDate);
            int likes = comment.GetKarma().GetLikesCount();
            likesView.SetText(String.ValueOf(likes));
            bool alreadyLiked = comment.GetKarma().GetCanLike() == Comment.CanLike.ALREADY_LIKED;
            likesCheckView.SetVisibility(alreadyLiked ? View.VISIBLE : View.GONE);
            contentView.SetText(Html.FromHtml(comment.GetContent()));
            itemView.SetOnClickListener((v) => EventBus_.GetInstance_(v.GetContext()).Post(new CommentActionsEvent(comment)));
        }
    }
}