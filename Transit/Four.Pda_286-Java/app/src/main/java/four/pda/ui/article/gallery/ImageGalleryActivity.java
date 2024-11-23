package four.pda.ui.article.gallery;

import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.viewpager.widget.ViewPager;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.Bean;
import org.androidannotations.annotations.EActivity;
import org.androidannotations.annotations.Extra;
import org.androidannotations.annotations.ViewById;

import java.util.ArrayList;

import four.pda.EventBus;
import four.pda.R;

/**
 * Created by asavinova on 01/09/16.
 */
@EActivity(R.layout.image_gallery_activity)
public class ImageGalleryActivity extends AppCompatActivity {

	@Extra String currentUrl;
	@Extra ArrayList<String> images;

	@ViewById View toolbar;
	@ViewById ImageView closeView;
	@ViewById TextView currentIndexView;
	@ViewById TextView imagesCountView;
	@ViewById ViewPager pager;

	@Bean EventBus eventBus;

	@AfterViews
	void afterViews() {

		closeView.setOnClickListener(v -> finish());

		int index = getCurrentImageIndex();
		currentIndexView.setText(String.valueOf(index + 1));
		imagesCountView.setText(String.valueOf(images.size()));

		pager.setAdapter(new ImagesPagerAdapter(this, images));
		pager.setCurrentItem(index, true);
		pager.addOnPageChangeListener(new ViewPager.SimpleOnPageChangeListener() {
			@Override
			public void onPageSelected(int position) {
				currentIndexView.setText(String.valueOf(position + 1));
			}
		});

	}

	@Override
	protected void onResume() {
		super.onResume();
		eventBus.register(this);
	}

	@Override
	protected void onPause() {
		eventBus.unregister(this);
		super.onPause();
	}

	public void onEvent(ImagesPagerAdapter.ImageClickEvent event) {
		toolbar.setVisibility(toolbar.getVisibility() == View.INVISIBLE ? View.VISIBLE : View.INVISIBLE);
	}

	public void onEvent(ImagesPagerAdapter.OutsideImageClickEvent event) {
		finish();
	}

	private int getCurrentImageIndex() {
		int index = 0;
		for (String image : images) {
			if (image.equals(currentUrl)) {
				break;
			}
			index++;
		}
		return index;
	}

}
