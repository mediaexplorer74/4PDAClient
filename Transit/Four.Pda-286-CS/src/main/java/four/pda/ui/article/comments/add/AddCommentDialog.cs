using Android.App;
using Android.Text;
using Android.View;
using Android.Widget;
using Androidx.Appcompat.Widget;
using Androidx.Fragment.App;
using Org.Androidannotations.Annotations;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using Four.Pda.Ui.Article.Comments;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Add
{
    /// <summary>
    /// Created by asavinova on 16/03/16.
    /// </summary>
    public class AddCommentDialog : DialogFragment
    {
        private static readonly int ADD_COMMENT_LOADER_ID = 0;
        long postId;
        long replyId;
        string replyAuthor;
        Toolbar toolbar;
        EditText messageEditText;
        SupportView supportView;
        FourPdaClient client;
        Keyboard keyboard;
        EventBus eventBus;
        Dao dao;
        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            toolbar.SetTitle(replyId == null ? R.@string.comments_new : R.@string.comments_reply_title);
            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => this.Dismiss());
            if (replyId != null)
            {
                string replyText = replyAuthor + ",\n";
                messageEditText.SetText(replyText);
                messageEditText.SetSelection(replyText.Length());
            }

            keyboard.ShowFor(messageEditText);
        }

        public override void OnStart()
        {
            base.OnStart();
            Dialog dialog = GetDialog();
            if (dialog != null)
            {

                // Максимально растягивает окно диалога
                dialog.GetWindow().SetLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.MATCH_PARENT);
            }
        }

        // Максимально растягивает окно диалога
        virtual void AddCommentClicked()
        {
            string message = messageEditText.GetText().ToString();
            if (TextUtils.IsEmpty(message))
            {
                messageEditText.SetError("Empty!");
            }
            else
            {
                AddComment();
            }
        }

        // Максимально растягивает окно диалога
        virtual void AddComment()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(ADD_COMMENT_LOADER_ID, null, new AddCommentCallbacks(this)).ForceLoad();
        }

        // Максимально растягивает окно диалога
        virtual void UpdateComments(CommentsContainer comments)
        {
            eventBus.Post(new UpdateCommentsEvent(comments));
            Dismiss();
        }
    }
}