package four.pda.dagger;

import dagger.Component;
import four.pda.ui.DrawerFragment;
import four.pda.ui.article.comments.add.AddCommentDialog;
import four.pda.ui.article.comments.actions.CommentActionsDialog;
import four.pda.ui.article.comments.CommentsFragment;
import four.pda.ui.article.list.ListFragment;
import four.pda.ui.article.one.ArticleFragment;
import four.pda.ui.article.one.ArticleTaskLoader;
import four.pda.ui.article.search.SearchFragment;
import four.pda.ui.auth.AuthActivity;
import four.pda.ui.profile.ProfileActivity;

/**
 * Created by asavinova on 23/02/16.
 */
@Component(modules = {ClientModule.class})
public interface FourPdaComponent {

	void inject(ListFragment x);

	void inject(ArticleFragment x);

	void inject(ArticleTaskLoader x);

	void inject(CommentsFragment x);

	void inject(AuthActivity x);

	void inject(DrawerFragment x);

	void inject(AddCommentDialog x);

	void inject(SearchFragment x);

	void inject(CommentActionsDialog x);

	void inject(ProfileActivity x);

}
