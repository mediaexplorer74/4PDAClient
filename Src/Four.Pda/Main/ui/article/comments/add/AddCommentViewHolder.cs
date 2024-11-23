using Android.View;
using Androidx.Recyclerview.Widget;
using Butterknife;
using Four.Pda;
using Four.Pda.Analytics;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Add
{
    /// <summary>
    /// Created by asavinova on 10/03/16.
    /// </summary>
    public class AddCommentViewHolder : ViewHolder
    {
        View addCommentButton;
        public AddCommentViewHolder(View view) : base(view)
        {
            ButterKnife.Bind(this, view);
            addCommentButton.SetOnClickListener((v) =>
            {
                Analytics_.GetInstance_(v.GetContext()).Comments().Add();
                EventBus_.GetInstance_(v.GetContext()).Post(new AddCommentEvent());
            });
        }
    }
}