using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Org.Androidannotations.Annotations;
using Org.Slf4j;
using Java.Util;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Dao;
using System.Diagnostics;

namespace Four.Pda
{
    /// <summary>
    /// Created by asavinova on 10/04/15.
    /// </summary>
    public class Dao
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(Dao));
        private SQLiteDatabase db;
        Context context;
        private DaoMaster.DevOpenHelper helper;
        private DaoMaster daoMaster;
        private DaoSession daoSession;
        virtual void Init()
        {
            helper = new DevOpenHelper(context, "app", null);
            db = helper.GetWritableDatabase();
            daoMaster = new DaoMaster(db);
            daoSession = daoMaster.NewSession();
        }

        public virtual void SetArticles(IList<ListArticle> articles, CategoryType category, bool needClearData)
        {
            daoSession.RunInTx(() =>
            {
                ArticleDao dao = daoSession.GetArticleDao();
                if (needClearData)
                {
                    dao.QueryBuilder().Where(ArticleDao.Properties.Category.Eq(GetCategoryValue(category))).BuildDelete().ExecuteDeleteWithoutDetachingEntities();
                    L.Trace("Delete all articles from category {}", category);
                }
                else
                {
                    L.Trace("No need clear for category {}", category);
                }

                foreach (ListArticle article in articles)
                {
                    Article daoArticle = new Article();
                    daoArticle.SetId(article.GetId());
                    daoArticle.SetDate(article.GetDate());
                    daoArticle.SetTitle(article.GetTitle());
                    daoArticle.SetImage(article.GetImage());
                    daoArticle.SetCategory(GetCategoryValue(category));
                    daoArticle.SetDescription(article.GetDescription());
                    daoArticle.SetPublishedDate(article.GetPublishedDate());
                    daoArticle.SetCommentsCount(article.GetCommentsCount());
                    if (article.GetLabel() != null)
                    {
                        daoArticle.SetLabelName(article.GetLabel().GetName());
                        daoArticle.SetLabelColor(article.GetLabel().GetColor());
                    }

                    User author = article.GetAuthor();

                    // В списке обзоров автор равен null
                    if (author != null)
                    {
                        daoArticle.SetAuthorId(author.GetId());
                        daoArticle.SetAuthorName(author.GetNickname());
                    }

                    dao.InsertOrReplace(daoArticle);
                }
            });
            L.Trace("All articles count = {}", daoSession.GetArticleDao().Count());
        }

        public virtual Cursor GetArticleCursor(CategoryType category)
        {
            ArticleDao dao = daoSession.GetArticleDao();
            return db.Query(ArticleDao.TABLENAME, dao.GetAllColumns(), ArticleDao.Properties.Category.columnName + " == '" + GetCategoryValue(category) + "'", null, null, null, ArticleDao.Properties.PublishedDate.columnName + " DESC");
        }

        public virtual void SetSearchArticles(IList<SearchListArticle> articles, bool needClearData)
        {
            daoSession.RunInTx(() =>
            {
                SearchArticleDao dao = daoSession.GetSearchArticleDao();
                if (needClearData)
                {
                    dao.DeleteAll();
                    L.Trace("Delete all search articles");
                }

                foreach (SearchListArticle listArticle in articles)
                {
                    SearchArticle article = new SearchArticle();
                    article.SetId(listArticle.GetId());
                    article.SetDate(listArticle.GetDate());
                    article.SetTitle(listArticle.GetTitle());
                    article.SetDescription(listArticle.GetDescription());
                    article.SetImage(listArticle.GetImage());
                    article.SetPosition(listArticle.GetPosition());
                    article.SetAuthorId(listArticle.GetAuthor().GetId());
                    article.SetAuthorName(listArticle.GetAuthor().GetNickname());
                    if (listArticle.GetLabel() != null)
                    {
                        article.SetLabelName(listArticle.GetLabel().GetName());
                        article.SetLabelColor(listArticle.GetLabel().GetColor());
                    }

                    dao.InsertOrReplace(article);
                }
            });
            L.Trace("All search articles count = {}", daoSession.GetSearchArticleDao().Count());
        }

        public virtual Cursor GetSearchArticleCursor()
        {
            SearchArticleDao dao = daoSession.GetSearchArticleDao();
            return db.Query(SearchArticleDao.TABLENAME, dao.GetAllColumns(), null, null, null, null, SearchArticleDao.Properties.Position.columnName + " ASC");
        }

        private string GetCategoryValue(CategoryType category)
        {
            return category.Name().ToLowerCase();
        }
    }
}