using Android.App;
using Android.Content;
using Android.Text;
using Android.View;
using Android.Widget;
using Androidx.Appcompat.Widget;
using Androidx.Core.App;
using Androidx.Fragment.App;
using Org.Androidannotations.Annotations;
using Java.Text;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui.Article.Comments.Add;
using Four.Pda.Ui.Auth;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Comments.Actions
{
    /// <summary>
    /// Created by asavinova on 25/04/16.
    /// </summary>
    public class CommentActionsDialog : DialogFragment
    {
        private static readonly SimpleDateFormat DATE_FORMAT = new SimpleDateFormat("dd.MM.yy HH:ss");
        private static readonly int LIKE_AUTH_REQUEST_CODE = 0;
        DialogParams params;
        Toolbar toolbar;
        TextView nickView;
        TextView dateView;
        View likesCheckView;
        TextView likesCountView;
        TextView contentView;
        TextView replyButton;
        View likeProgressView;
        ImageView likeButton;
        EventBus eventBus;
        Analytics analytics;
        FourPdaClient client;
        virtual void AfterViews()
        {
            ((App)GetContext().GetApplicationContext()).Component().Inject(this);
            analytics.Comments().ShowDialog();
            toolbar.SetTitle(R.@string.show_comment_dialog_title);
            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => this.Dismiss());
            nickView.SetText(@params.AuthorName());
            string verboseDate = DATE_FORMAT.Format(@params.Date());
            dateView.SetText(verboseDate);
            contentView.SetText(Html.FromHtml(@params.Content()));
            replyButton.SetVisibility(@params.CanReply() ? View.VISIBLE : View.GONE);
            UpdateLikes();
        }

        virtual void Share()
        {
            analytics.Comments().Share();
            StartActivity(ShareCompat.IntentBuilder.From(GetActivity()).SetType("text/plain").SetText(client.GetCommentUrl(@params.ArticleId(), @params.ArticleDate(), @params.Id())).CreateChooserIntent());
            Dismiss();
        }

        virtual void LikeButton()
        {
            analytics.Comments().Like();
            StartActivityForResult(new Intent(GetActivity(), typeof(AuthActivity_)), LIKE_AUTH_REQUEST_CODE);
        }

        virtual void Reply()
        {
            analytics.Comments().Reply();
            eventBus.Post(new AddCommentEvent(@params.Id(), @params.AuthorName()));
            Dismiss();
        }

        virtual void Profile()
        {
            analytics.Comments().ProfileClicked();
            ProfileActivity_.Intent(GetActivity()).ProfileId(@params.AuthorId()).Start();
        }

        virtual void OnResult(int resultCode)
        {
            if (Activity.RESULT_OK == resultCode)
            {
                Like();
            }
        }

        virtual void Like()
        {
            switch (@params.CanLike())
            {
                case ALREADY_LIKED:
                    Toast.MakeText(GetContext(), R.@string.article_comment_like_already, Toast.LENGTH_SHORT).Show();
                    break;
                case CAN:
                    likeButton.SetVisibility(View.INVISIBLE);
                    likeProgressView.SetVisibility(View.VISIBLE);
                    GetLoaderManager().InitLoader(0, null, new LikeCommentLoaderCallbacks(this, GetContext(), client)).ForceLoad();
                    break;
                default:
                    throw new InvalidOperationException("Unexpected CanLike value " + @params.CanLike().Name());
                    break;
            }
        }

        virtual void UpdateLikes()
        {
            likesCountView.SetText(String.ValueOf(@params.LikeCount()));
            likesCheckView.SetVisibility(@params.CanLike() == Comment.CanLike.ALREADY_LIKED ? View.VISIBLE : View.GONE);
            likeButton.SetVisibility(@params.CanLike() == Comment.CanLike.CANT ? View.INVISIBLE : View.VISIBLE);
        }
    }
}