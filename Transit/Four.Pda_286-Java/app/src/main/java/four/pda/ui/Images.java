package four.pda.ui;

import android.widget.ImageView;

import com.squareup.picasso.Callback;
import com.squareup.picasso.Picasso;

import uk.co.senab.photoview.PhotoViewAttacher;

/**
 * Created by asavinova on 10/04/15.
 */
public class Images {

	public static void load(ImageView view, String url) {
		load(view, url, null);
	}

	public static void load(final ImageView view, String url, Callback callback) {
		Picasso.with(view.getContext())
				.load(url)
				.fit()
				.centerInside()
				.into(view, callback);
	}

	public static void loadWithZoom(final ImageView view, String url, final PhotoViewAttacher attacher) {
		Callback callback = new Callback() {
			@Override
			public void onSuccess() {
				attacher.update();
			}

			@Override
			public void onError() {
			}
		};
		load(view, url, callback);
	}

}
