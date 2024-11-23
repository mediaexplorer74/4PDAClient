using Android.Content;
using Androidx.Loader.Content;
using Org.Slf4j;
using Java.Util;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.One
{
    /// <summary>
    /// Created by asavinova on 12/04/15.
    /// </summary>
    public class ArticleTaskLoader : AsyncTaskLoader<LoadResult<ArticleContent>>
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(ArticleTaskLoader));
        FourPdaClient client;
        private long id;
        private Date date;
        public ArticleTaskLoader(Context context, FourPdaClient client, long id, Date date) : base(context)
        {
            ((App)context.GetApplicationContext()).Component().Inject(this);
            this.client = client;
            this.id = id;
            this.date = date;
        }

        protected override void OnStartLoading()
        {
            base.OnStartLoading();
            ForceLoad();
        }

        public override LoadResult<ArticleContent> LoadInBackground()
        {
            try
            {
                return new LoadResult(client.GetArticleContent(date, id));
            }
            catch (Exception e)
            {
                L.Error("Article request error", e);
                return new LoadResult(e);
            }
        }
    }
}