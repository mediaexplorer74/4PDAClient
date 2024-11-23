using Dagger;
using Four.Pda.Ui;
using Four.Pda.Ui.Article.Comments.Add;
using Four.Pda.Ui.Article.Comments.Actions;
using Four.Pda.Ui.Article.Comments;
using Four.Pda.Ui.Article.List;
using Four.Pda.Ui.Article.One;
using Four.Pda.Ui.Article.Search;
using Four.Pda.Ui.Auth;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Dagger
{
    /// <summary>
    /// Created by asavinova on 23/02/16.
    /// </summary>
    public interface FourPdaComponent
    {
        void Inject(ListFragment x);
        void Inject(ArticleFragment x);
        void Inject(ArticleTaskLoader x);
        void Inject(CommentsFragment x);
        void Inject(AuthActivity x);
        void Inject(DrawerFragment x);
        void Inject(AddCommentDialog x);
        void Inject(SearchFragment x);
        void Inject(CommentActionsDialog x);
        void Inject(ProfileActivity x);
    }
}