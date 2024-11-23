using Android.Content;
using Android.Net;
using Android.Os;
using Android.View;
using Android.Webkit;
using Android.Widget;
using Androidx.Appcompat.Widget;
using Androidx.Core.App;
using Androidx.Loader.App;
using Androidx.Loader.Content;
using Androidx.Swiperefreshlayout.Widget;
using Net.Opacapp.Multilinecollapsingtoolbar;
using Org.Androidannotations.Annotations;
using Org.Androidannotations.Annotations.Sharedpreferences;
using Java.Util;
using Javax.Inject;
using Four.Pda;
using Four.Pda.Analytics;
using Four.Pda.Client;
using Four.Pda.Client.Model;
using Four.Pda.Template;
using Four.Pda.Ui;
using Four.Pda.Ui.Article;
using Four.Pda.Ui.Article.Gallery;
using Four.Pda.Ui.Profile;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.One
{
    /// <summary>
    /// Created by asavinova on 11/04/15.
    /// </summary>
    public class ArticleFragment : BaseFragment, OnRefreshListener
    {
        private static readonly int LOADER_ID = 0;
        long id;
        Date date;
        string title;
        string image;
        long authorId;
        string authorName;
        string labelName;
        string labelColor;
        Toolbar toolbar;
        CollapsingToolbarLayout collapsingToolbar;
        AspectRatioImageView backdropImageView;
        AspectRatioImageView backdropImageShadowView;
        LabelView labelView;
        TextView authorView;
        TextView dateView;
        WebView webView;
        SupportView supportView;
        TextZoomPanel textZoomPanel;
        TextView commentsCountView;
        Dao dao;
        EventBus eventBus;
        Analytics analytics;
        Preferences_ preferences;
        FourPdaClient client;
        private NewsArticleTemplate articleTemplate = new NewsArticleTemplate();
        virtual void AfterViews()
        {
            ((App)GetActivity().GetApplication()).Component().Inject(this);
            analytics.Article().Open();
            webView.GetSettings().SetJavaScriptEnabled(true);
            webView.GetSettings().SetTextZoom(preferences.TextZoom().Get());
            webView.GetSettings().SetAppCacheEnabled(false);
            webView.GetSettings().SetCacheMode(WebSettings.LOAD_NO_CACHE);
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT && BuildConfig.DEBUG)
            {
                WebView.SetWebContentsDebuggingEnabled(true);
            }

            toolbar.SetNavigationIcon(R.drawable.ic_close_white_24dp);
            toolbar.SetNavigationOnClickListener((v) => GetActivity().OnBackPressed());
            toolbar.InflateMenu(R.menu.article);
            toolbar.GetMenu().FindItem(R.id.text_zoom).SetOnMenuItemClickListener((item) =>
            {
                analytics.Article().TextZoomOpen();
                textZoomPanel.SetZoom(preferences.TextZoom().Get());
                textZoomPanel.SetVisibility(View.VISIBLE);
                return true;
            });
            toolbar.GetMenu().FindItem(R.id.share).SetOnMenuItemClickListener((item) =>
            {
                analytics.Article().Share();
                StartActivity(ShareCompat.IntentBuilder.From(GetActivity()).SetType("text/plain").SetText(client.GetArticleUrl(date, id)).CreateChooserIntent());
                return true;
            });
            collapsingToolbar.SetTitle(title);
            Images.Load(backdropImageView, image);
            GetView().GetViewTreeObserver().AddOnGlobalLayoutListener(new AnonymousOnGlobalLayoutListener(this));
            labelView.SetLabel(labelName, labelColor);
            authorView.SetText(authorName);
            dateView.SetText(DateFormats.VERBOSE.Format(date));
            LoadData();
        }

        private sealed class AnonymousOnGlobalLayoutListener : OnGlobalLayoutListener
        {
            public AnonymousOnGlobalLayoutListener(ArticleFragment parent)
            {
                this.parent = parent;
            }

            private readonly ArticleFragment parent;
            public void OnGlobalLayout()
            {
                GetView().GetViewTreeObserver().RemoveOnGlobalLayoutListener(this);
                int width = GetView().GetWidth();
                int height = GetView().GetHeight();
                float k = (float)width / height;
                if (k > 1)
                {
                    k = 0.75F / k;
                }

                if (k < 0.5)
                {
                    k = 0.5F;
                }

                backdropImageView.SetAspectRatio(k);
                backdropImageShadowView.SetAspectRatio(k * 0.6F);
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            eventBus.Register(this);
        }

        public override void OnPause()
        {
            base.OnPause();
            eventBus.Unregister(this);
        }

        virtual void CommentsButton()
        {
            eventBus.Post(new ShowArticleCommentsEvent(id, date));
        }

        virtual void AuthorClicked()
        {

            // При переходе на статью из категории обзоров автора не будет
            if (authorId > 0)
            {
                analytics.Article().ProfileClicked();
                ProfileActivity_.Intent(this).ProfileId(authorId).Start();
            }
        }

        // При переходе на статью из категории обзоров автора не будет
        private void LoadData()
        {
            supportView.ShowProgress();
            GetLoaderManager().RestartLoader(LOADER_ID, null, new Callbacks());
        }

        // При переходе на статью из категории обзоров автора не будет
        public override void OnRefresh()
        {
            LoadData();
        }

        // При переходе на статью из категории обзоров автора не будет
        virtual void UpdateData(ArticleContent article)
        {

            //TODO Show author name from ArticleContent for reviews
            if (article.GetLabel() != null)
            {
                AbstractArticle.Label label = article.GetLabel();
                labelView.SetLabel(label.GetName(), label.GetColor());
            }

            webView.SetWebChromeClient(new WebChromeClient());
            webView.SetWebViewClient(new AnonymousWebViewClient(this));
            webView.LoadData(GetFormattedText(article.GetContent()), "text/html; charset=utf-8", null);
            webView.RequestFocus();
        }

        private sealed class AnonymousWebViewClient : WebViewClient
        {
            public AnonymousWebViewClient(ArticleFragment parent)
            {
                this.parent = parent;
            }

            private readonly ArticleFragment parent;
            public bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                if (IsGalleryImage(article.GetImages(), url))
                {
                    OpenImageGallery(article.GetImages(), url);
                }
                else
                {
                    OpenActionViewIntent(url);
                }

                return true;
            }
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        private bool IsGalleryImage(IList<string> images, string url)
        {
            foreach (string image in images)
            {
                if (image.Equals(url))
                {
                    return true;
                }
            }

            return false;
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        private void OpenImageGallery(IList<string> images, string url)
        {
            analytics.Article().OpenImageGallery();
            ImageGalleryActivity_.Intent(this).CurrentUrl(url).Images(new List(images)).Start();
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        private void OpenActionViewIntent(string url)
        {
            Intent intent = new Intent(Intent.ACTION_VIEW, Uri.Parse(url));
            try
            {
                StartActivity(intent);
            }
            catch (android.content.ActivityNotFoundException ex)
            {
                Toast.MakeText(GetActivity(), R.@string.no_content_applications_installed, Toast.LENGTH_SHORT).Show();
            }
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        private string GetFormattedText(string content)
        {
            return articleTemplate.Make(content);
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        public virtual void OnEvent(SetTextZoomEvent @event)
        {
            webView.GetSettings().SetTextZoom(@event.GetZoom());
        }

        // При переходе на статью из категории обзоров автора не будет
        //TODO Show author name from ArticleContent for reviews
        class Callbacks : LoaderCallbacks<LoadResult<ArticleContent>>
        {
            public virtual Loader<LoadResult<ArticleContent>> OnCreateLoader(int loaderId, Bundle args)
            {
                return new ArticleTaskLoader(GetActivity(), client, id, date);
            }

            public virtual void OnLoadFinished(Loader<LoadResult<ArticleContent>> loader, LoadResult<ArticleContent> result)
            {
                if (result.GetException() != null)
                {
                    supportView.ShowError(GetString(R.@string.article_network_error), (v) => LoadData());
                    return;
                }

                UpdateData(result.GetData());
                int commentsCount = result.GetData().GetCommentsCount();
                commentsCountView.SetText(String.ValueOf(commentsCount));
                supportView.Hide();
            }

            public virtual void OnLoaderReset(Loader<LoadResult<ArticleContent>> loader)
            {
            }
        }
    }
}