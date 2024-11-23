package four.pda.ui.article.gallery;

import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import androidx.annotation.NonNull;
import androidx.viewpager.widget.PagerAdapter;

import java.util.ArrayList;
import java.util.List;

import four.pda.EventBus_;
import four.pda.ui.DoubleTapListener;
import four.pda.ui.Images;
import uk.co.senab.photoview.PhotoViewAttacher;

/**
 * Created by asavinova on 01/09/16.
 */
public class ImagesPagerAdapter extends PagerAdapter {

	private Context context;
	private List<String> images = new ArrayList<>();

	public ImagesPagerAdapter(Context context, List<String> images) {
		this.context = context;
		this.images = images;
	}

	@Override
	public int getCount() {
		return images.size();
	}

	@Override
	public boolean isViewFromObject(View view, Object object) {
		return view == object;
	}

	@Override
	public Object instantiateItem(ViewGroup collection, int position) {
		ImageView imageView = new ImageView(context);
		Images.loadWithZoom(imageView, images.get(position), getPhotoViewAttacher(imageView));
		collection.addView(imageView);
		return imageView;
	}

	@NonNull
	private PhotoViewAttacher getPhotoViewAttacher(ImageView imageView) {

		PhotoViewAttacher attacher = new PhotoViewAttacher(imageView);
		attacher.setScaleLevels(1, 3, 8);
		attacher.setOnDoubleTapListener(new DoubleTapListener(attacher));
		attacher.setOnPhotoTapListener(new PhotoViewAttacher.OnPhotoTapListener() {
			@Override
			public void onPhotoTap(View view, float x, float y) {
				EventBus_.getInstance_(view.getContext()).post(new ImageClickEvent());
			}

			@Override
			public void onOutsidePhotoTap() {
				EventBus_.getInstance_(context).post(new OutsideImageClickEvent());
			}
		});

		return attacher;
	}

	@Override
	public void destroyItem(ViewGroup collection, int position, Object view) {
		collection.removeView((View) view);
	}

	public class ImageClickEvent {
	}

	public class OutsideImageClickEvent {
	}

}
