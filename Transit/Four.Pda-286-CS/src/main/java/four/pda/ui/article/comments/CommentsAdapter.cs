using Android.Content;
using Android.View;
using Androidx.Recyclerview.Widget;
using Java.Util;
using Four.Pda;
using Four.Pda.Client.Model;
using Four.Pda.Ui.Article.Comments.Add;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments
{
    /// <summary>
    /// Created by asavinova on 05/12/15.
    /// </summary>
    class CommentsAdapter : Adapter<RecyclerView.ViewHolder>
    {
        enum Type
        {
            REGULAR,
            DELETED,
            ADD
        }

        private readonly LayoutInflater inflater;
        private IList<AbstractComment> comments = new List();
        private bool canAddNewComment = false;
        private int viewWidth;
        public CommentsAdapter(Context context)
        {
            inflater = LayoutInflater.From(context);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == Type.ADD.Ordinal())
            {
                View view = inflater.Inflate(R.layout.add_comment_item, parent, false);
                return new AddCommentViewHolder(view);
            }

            if (viewType == Type.DELETED.Ordinal())
            {
                View view = inflater.Inflate(R.layout.deleted_comment_item, parent, false);
                return new ViewHolder(view);
            }

            View view = inflater.Inflate(R.layout.comment_list_item, parent, false);
            return new CommentViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            int type = GetItemViewType(position);
            if (type == Type.ADD.Ordinal())
            {
                return;
            }

            AbstractComment abstractComment = comments[position];
            if (type == Type.REGULAR.Ordinal())
            {
                ((CommentViewHolder)holder).SetComment((Comment)abstractComment);
            }

            int left = viewWidth / 30 * abstractComment.GetLevel();
            holder.itemView.SetPadding(left, 0, 0, 0);
        }

        public override int GetItemCount()
        {
            return comments.Count + (canAddNewComment ? 1 : 0);
        }

        public override int GetItemViewType(int position)
        {
            if (position >= comments.Count)
            {
                return Type.ADD.Ordinal();
            }

            if (comments[position] is DeletedComment)
            {
                return Type.DELETED.Ordinal();
            }

            return Type.REGULAR.Ordinal();
        }

        public virtual void SetViewWidth(int width)
        {
            this.viewWidth = width;
        }

        public virtual void SetCommentsContainer(CommentsContainer container)
        {
            this.comments = new List();
            canAddNewComment = container.CanAddNewComment();
            AddComments(container.GetComments());
        }

        public virtual void LikeChanged(long commentId, int likesCount)
        {
            for (int i = 0; i < comments.Count; i++)
            {
                AbstractComment abstractComment = comments[i];
                if (!(abstractComment is Comment))
                {
                    continue;
                }

                Comment comment = (Comment)abstractComment;
                if (comment.GetId() == commentId)
                {
                    comment.GetKarma().SetCanLike(Comment.CanLike.ALREADY_LIKED);
                    comment.GetKarma().SetLikesCount(likesCount);
                    NotifyItemChanged(i);
                    break;
                }
            }
        }

        private void AddComments(IList<AbstractComment> tree)
        {
            if (tree == null)
                return;
            foreach (AbstractComment comment in tree)
            {
                comments.Add(comment);
                AddComments(comment.GetChildren());
            }
        }
    }
}