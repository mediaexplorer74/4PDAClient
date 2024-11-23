package four.pda.ui;

import android.content.Context;
import android.util.AttributeSet;
import android.view.MotionEvent;

import androidx.viewpager.widget.ViewPager;

/**
 * <a href="https://github.com/chrisbanes/PhotoView/issues/31">
 * IllegalArgumentException (pointerIndex out of range)
 * while using many fingers to zoom in and out
 * </a>
 */
public class SafeViewPager extends ViewPager {

	public SafeViewPager(Context context) {
		super(context);
	}

	public SafeViewPager(Context context, AttributeSet attrs) {
		super(context, attrs);
	}

	@Override
	public boolean onTouchEvent(MotionEvent ev) {
		try {
			return super.onTouchEvent(ev);
		} catch (IllegalArgumentException ex) {
			ex.printStackTrace();
		}
		return false;
	}

	@Override
	public boolean onInterceptTouchEvent(MotionEvent ev) {
		try {
			return super.onInterceptTouchEvent(ev);
		} catch (IllegalArgumentException ex) {
			ex.printStackTrace();
		}
		return false;
	}

}
