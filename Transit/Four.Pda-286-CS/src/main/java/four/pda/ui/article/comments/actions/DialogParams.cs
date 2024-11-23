using Android.Os;
using Com.Google.Auto.Value;
using Java.Util;
using Four.Pda.Client.Model;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Actions
{
    /// <summary>
    /// Created by pavel on 07/06/16.
    /// </summary>
    public abstract class DialogParams : Parcelable
    {
        abstract long Id();
        abstract long AuthorId();
        abstract string AuthorName();
        abstract Date Date();
        abstract Comment.CanLike CanLike();
        abstract int LikeCount();
        abstract string Content();
        abstract bool CanReply();
        abstract long ArticleId();
        abstract Date ArticleDate();
        public static DialogParams Create(Comment comment, long articleId, Date articleDate)
        {
            return new AutoValue_DialogParams(comment.GetId(), comment.GetUser().GetId(), comment.GetUser().GetNickname(), comment.GetDate(), comment.GetKarma().GetCanLike(), comment.GetKarma().GetLikesCount(), comment.GetContent(), comment.CanReply(), articleId, articleDate);
        }
    }
}