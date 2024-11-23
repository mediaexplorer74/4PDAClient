package four.pda.ui.article.one;

import android.content.Intent;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.view.ViewTreeObserver;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.widget.Toolbar;
import androidx.core.app.ShareCompat;
import androidx.loader.app.LoaderManager;
import androidx.loader.content.Loader;
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout;

import net.opacapp.multilinecollapsingtoolbar.CollapsingToolbarLayout;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.Click;
import org.androidannotations.annotations.EFragment;
import org.androidannotations.annotations.FragmentArg;
import org.androidannotations.annotations.UiThread;
import org.androidannotations.annotations.ViewById;
import org.androidannotations.annotations.sharedpreferences.Pref;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.inject.Inject;

import four.pda.App;
import four.pda.BuildConfig;
import four.pda.Dao;
import four.pda.DateFormats;
import four.pda.EventBus;
import four.pda.Preferences_;
import four.pda.R;
import four.pda.analytics.Analytics;
import four.pda.client.FourPdaClient;
import four.pda.client.model.AbstractArticle;
import four.pda.client.model.ArticleContent;
import four.pda.template.NewsArticleTemplate;
import four.pda.ui.AspectRatioImageView;
import four.pda.ui.BaseFragment;
import four.pda.ui.Images;
import four.pda.ui.LoadResult;
import four.pda.ui.SupportView;
import four.pda.ui.article.LabelView;
import four.pda.ui.article.ShowArticleCommentsEvent;
import four.pda.ui.article.gallery.ImageGalleryActivity_;
import four.pda.ui.profile.ProfileActivity_;

/**
 * Created by asavinova on 11/04/15.
 */
@EFragment(R.layout.article_one)
public class ArticleFragment extends BaseFragment implements SwipeRefreshLayout.OnRefreshListener {

	private static final int LOADER_ID = 0;

	@FragmentArg long id;
	@FragmentArg Date date;
	@FragmentArg String title;
	@FragmentArg String image;
	@FragmentArg long authorId;
	@FragmentArg String authorName;
	@FragmentArg String labelName;
	@FragmentArg String labelColor;

	@ViewById Toolbar toolbar;
	@ViewById CollapsingToolbarLayout collapsingToolbar;
	@ViewById AspectRatioImageView backdropImageView;
	@ViewById AspectRatioImageView backdropImageShadowView;

	@ViewById LabelView labelView;

	@ViewById TextView authorView;
	@ViewById TextView dateView;

	@ViewById WebView webView;

	@ViewById SupportView supportView;
	@ViewById TextZoomPanel textZoomPanel;
	@ViewById TextView commentsCountView;

	@Bean Dao dao;
	@Bean EventBus eventBus;
	@Bean Analytics analytics;

	@Pref Preferences_ preferences;

	@Inject FourPdaClient client;

	private NewsArticleTemplate articleTemplate = new NewsArticleTemplate();

	@AfterViews
	void afterViews() {
		((App) getActivity().getApplication()).component().inject(this);

		analytics.article().open();

		webView.getSettings().setJavaScriptEnabled(true);
		webView.getSettings().setTextZoom(preferences.textZoom().get());
		webView.getSettings().setAppCacheEnabled(false);
		webView.getSettings().setCacheMode(WebSettings.LOAD_NO_CACHE);

		if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT && BuildConfig.DEBUG) {
			WebView.setWebContentsDebuggingEnabled(true);
		}

		toolbar.setNavigationIcon(R.drawable.ic_close_white_24dp);
		toolbar.setNavigationOnClickListener(v -> getActivity().onBackPressed());

		toolbar.inflateMenu(R.menu.article);
		toolbar.getMenu().findItem(R.id.text_zoom).setOnMenuItemClickListener(item -> {
			analytics.article().textZoomOpen();
			textZoomPanel.setZoom(preferences.textZoom().get());
			textZoomPanel.setVisibility(View.VISIBLE);
			return true;
		});

		toolbar.getMenu().findItem(R.id.share).setOnMenuItemClickListener(item -> {
			analytics.article().share();
			startActivity(ShareCompat.IntentBuilder.from(getActivity())
					.setType("text/plain")
					.setText(client.getArticleUrl(date, id))
					.createChooserIntent());
			return true;
		});

		collapsingToolbar.setTitle(title);
		Images.load(backdropImageView, image);

		getView().getViewTreeObserver().addOnGlobalLayoutListener(new ViewTreeObserver.OnGlobalLayoutListener() {
			@Override
			public void onGlobalLayout() {
				getView().getViewTreeObserver().removeOnGlobalLayoutListener(this);

				int width = getView().getWidth();
				int height = getView().getHeight();

				float k = (float) width / height;

				if (k > 1) {
					k = 0.75f / k;
				}

				if (k < 0.5) {
					k = 0.5f;
				}

				backdropImageView.setAspectRatio(k);
				backdropImageShadowView.setAspectRatio(k * 0.6f);
			}
		});

		labelView.setLabel(labelName, labelColor);

		authorView.setText(authorName);
		dateView.setText(DateFormats.VERBOSE.format(date));

		loadData();
	}

	@Override
	public void onResume() {
		super.onResume();
		eventBus.register(this);
	}

	@Override
	public void onPause() {
		super.onPause();
		eventBus.unregister(this);
	}

	@Click
	void commentsButton() {
		eventBus.post(new ShowArticleCommentsEvent(id, date));
	}

	@Click(R.id.author_view)
	void authorClicked() {
		// При переходе на статью из категории обзоров автора не будет
		if (authorId > 0) {
			analytics.article().profileClicked();
			ProfileActivity_.intent(this)
					.profileId(authorId)
					.start();
		}
	}

	private void loadData() {
		supportView.showProgress();
		getLoaderManager().restartLoader(LOADER_ID, null, new Callbacks());
	}

	@Override
	public void onRefresh() {
		loadData();
	}

	@UiThread
	void updateData(final ArticleContent article) {

		//TODO Show author name from ArticleContent for reviews

		if (article.getLabel() != null) {
			AbstractArticle.Label label = article.getLabel();
			labelView.setLabel(label.getName(), label.getColor());
		}

		webView.setWebChromeClient(new WebChromeClient());
		webView.setWebViewClient(new WebViewClient() {
			@Override
			public boolean shouldOverrideUrlLoading(WebView view, String url) {
				if (isGalleryImage(article.getImages(), url)) {
					openImageGallery(article.getImages(), url);
				} else {
					openActionViewIntent(url);
				}
				return true;
			}
		});
		webView.loadData(getFormattedText(article.getContent()), "text/html; charset=utf-8", null);
		webView.requestFocus();
	}

	private boolean isGalleryImage(List<String> images, String url) {
		for (String image : images) {
			if (image.equals(url)) {
				return true;
			}
		}
		return false;
	}

	private void openImageGallery(List<String> images, String url) {
		analytics.article().openImageGallery();
		ImageGalleryActivity_.intent(this)
				.currentUrl(url)
				.images(new ArrayList<>(images))
				.start();
	}

	private void openActionViewIntent(String url) {
		Intent intent = new Intent(Intent.ACTION_VIEW, Uri.parse(url));
		try {
			startActivity(intent);
		} catch (android.content.ActivityNotFoundException ex) {
			Toast.makeText(getActivity(), R.string.no_content_applications_installed, Toast.LENGTH_SHORT).show();
		}
	}

	private String getFormattedText(String content) {
		return articleTemplate.make(content);
	}

	public void onEvent(SetTextZoomEvent event) {
		webView.getSettings().setTextZoom(event.getZoom());
	}

	class Callbacks implements LoaderManager.LoaderCallbacks<LoadResult<ArticleContent>> {

		@Override
		public Loader<LoadResult<ArticleContent>> onCreateLoader(int loaderId, final Bundle args) {
			return new ArticleTaskLoader(getActivity(), client, id, date);
		}

		@Override
		public void onLoadFinished(Loader<LoadResult<ArticleContent>> loader, LoadResult<ArticleContent> result) {

			if (result.getException() != null) {
				supportView.showError(getString(R.string.article_network_error), v -> loadData());
				return;
			}

			updateData(result.getData());

			int commentsCount = result.getData().getCommentsCount();
			commentsCountView.setText(String.valueOf(commentsCount));

			supportView.hide();
		}

		@Override
		public void onLoaderReset(Loader<LoadResult<ArticleContent>> loader) {
		}

	}

}
