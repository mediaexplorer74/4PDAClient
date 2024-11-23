package four.pda.ui.article;

import android.content.Context;
import android.graphics.Color;
import android.util.AttributeSet;
import android.view.View;
import android.widget.TextView;

import org.apache.commons.lang3.StringUtils;

/**
 * @author Pavel Savinov (swapii@gmail.com)
 */
public class LabelView extends TextView {

	public LabelView(Context context) {
		super(context);
		init();
	}

	public LabelView(Context context, AttributeSet attrs) {
		super(context, attrs);
		init();
	}

	public LabelView(Context context, AttributeSet attrs, int defStyleAttr) {
		super(context, attrs, defStyleAttr);
		init();
	}

	private void init() {
		float density = getResources().getDisplayMetrics().density;
		int verticalPadding = (int) (4 * density);
		int horizontalPadding = (int) (8 * density);
		setPadding(horizontalPadding, verticalPadding, horizontalPadding, verticalPadding);
		setTextColor(Color.WHITE);
	}

	public void setLabel(String name, String color) {

		boolean isVisible = StringUtils.isNotBlank(name);
		setVisibility(isVisible ? View.VISIBLE : View.GONE);

		if (!isVisible) {
			return;
		}

		setText(name);
		setBackgroundColor(LabelColor.getColorValueByName(color));
	}

}
