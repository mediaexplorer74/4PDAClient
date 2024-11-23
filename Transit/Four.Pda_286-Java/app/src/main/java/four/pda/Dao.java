package four.pda;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import org.androidannotations.annotations.AfterInject;
import org.androidannotations.annotations.EBean;
import org.androidannotations.annotations.RootContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.List;

import four.pda.client.CategoryType;
import four.pda.client.model.ListArticle;
import four.pda.client.model.SearchListArticle;
import four.pda.client.model.User;
import four.pda.dao.Article;
import four.pda.dao.ArticleDao;
import four.pda.dao.DaoMaster;
import four.pda.dao.DaoSession;
import four.pda.dao.SearchArticle;
import four.pda.dao.SearchArticleDao;

/**
 * Created by asavinova on 10/04/15.
 */
@EBean(scope = EBean.Scope.Singleton)
public class Dao {

	private static final Logger L = LoggerFactory.getLogger(Dao.class);

	private SQLiteDatabase db;
	@RootContext Context context;

	private DaoMaster.DevOpenHelper helper;
	private DaoMaster daoMaster;
	private DaoSession daoSession;

	@AfterInject
	void init() {
		helper = new DaoMaster.DevOpenHelper(context, "app", null);
		db = helper.getWritableDatabase();
		daoMaster = new DaoMaster(db);
		daoSession = daoMaster.newSession();
	}

	public void setArticles(final List<ListArticle> articles, final CategoryType category, final boolean needClearData) {
		daoSession.runInTx(() -> {
			ArticleDao dao = daoSession.getArticleDao();

			if (needClearData) {
				dao.queryBuilder()
						.where(ArticleDao.Properties.Category.eq(getCategoryValue(category)))
						.buildDelete()
						.executeDeleteWithoutDetachingEntities();
				L.trace("Delete all articles from category {}", category);
			} else {
				L.trace("No need clear for category {}", category);
			}

			for (ListArticle article : articles) {

				Article daoArticle = new Article();
				daoArticle.setId(article.getId());
				daoArticle.setDate(article.getDate());
				daoArticle.setTitle(article.getTitle());
				daoArticle.setImage(article.getImage());
				daoArticle.setCategory(getCategoryValue(category));
				daoArticle.setDescription(article.getDescription());
				daoArticle.setPublishedDate(article.getPublishedDate());
				daoArticle.setCommentsCount(article.getCommentsCount());

				if (article.getLabel() != null) {
					daoArticle.setLabelName(article.getLabel().getName());
					daoArticle.setLabelColor(article.getLabel().getColor());
				}

				User author = article.getAuthor();
				// В списке обзоров автор равен null
				if (author != null) {
					daoArticle.setAuthorId(author.getId());
					daoArticle.setAuthorName(author.getNickname());
				}

				dao.insertOrReplace(daoArticle);
			}
		});

		L.trace("All articles count = {}", daoSession.getArticleDao().count());
	}

	public Cursor getArticleCursor(CategoryType category) {
		ArticleDao dao = daoSession.getArticleDao();
		return db.query(ArticleDao.TABLENAME,
				dao.getAllColumns(),
				ArticleDao.Properties.Category.columnName + " == '" + getCategoryValue(category) + "'",
				null,
				null,
				null,
				ArticleDao.Properties.PublishedDate.columnName + " DESC");
	}

	public void setSearchArticles(final List<SearchListArticle> articles, final boolean needClearData) {
		daoSession.runInTx(() -> {
			SearchArticleDao dao = daoSession.getSearchArticleDao();

			if (needClearData) {
				dao.deleteAll();
				L.trace("Delete all search articles");
			}

			for (SearchListArticle listArticle : articles) {

				SearchArticle article = new SearchArticle();
				article.setId(listArticle.getId());
				article.setDate(listArticle.getDate());
				article.setTitle(listArticle.getTitle());
				article.setDescription(listArticle.getDescription());
				article.setImage(listArticle.getImage());
				article.setPosition(listArticle.getPosition());
				article.setAuthorId(listArticle.getAuthor().getId());
				article.setAuthorName(listArticle.getAuthor().getNickname());

				if (listArticle.getLabel() != null) {
					article.setLabelName(listArticle.getLabel().getName());
					article.setLabelColor(listArticle.getLabel().getColor());
				}

				dao.insertOrReplace(article);
			}
		});

		L.trace("All search articles count = {}", daoSession.getSearchArticleDao().count());
	}

	public Cursor getSearchArticleCursor() {
		SearchArticleDao dao = daoSession.getSearchArticleDao();
		return db.query(SearchArticleDao.TABLENAME,
				dao.getAllColumns(),
				null,
				null,
				null,
				null,
				SearchArticleDao.Properties.Position.columnName + " ASC");
	}

	private String getCategoryValue(CategoryType category) {
		return category.name().toLowerCase();
	}

}
