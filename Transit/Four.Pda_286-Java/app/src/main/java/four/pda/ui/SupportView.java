package four.pda.ui;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.rey.material.widget.ProgressView;

import org.androidannotations.annotations.AfterViews;
import org.androidannotations.annotations.EViewGroup;
import org.androidannotations.annotations.UiThread;
import org.androidannotations.annotations.ViewById;

import four.pda.R;

/**
 * Created by asavinova on 06/05/15.
 */
@EViewGroup(R.layout.support_view)
public class SupportView extends FrameLayout {

	@ViewById ProgressView progressView;
	@ViewById LinearLayout errorPanel;
	@ViewById TextView errorMessage;
	@ViewById TextView retryView;

	private int errorTextColor;

	public SupportView(Context context) {
		super(context);
	}

	public SupportView(Context context, AttributeSet attrs) {
		super(context, attrs);
		init(attrs);
	}

	public SupportView(Context context, AttributeSet attrs, int defStyleAttr) {
		super(context, attrs, defStyleAttr);
		init(attrs);
	}

	private void init(AttributeSet attrs) {
		TypedArray typedArray = getContext().obtainStyledAttributes(attrs, R.styleable.SupportView);
		{
			int defaultColor;
			if (android.os.Build.VERSION.SDK_INT >= 23) {
				defaultColor = getResources().getColor(android.R.color.primary_text_light, null);
			} else {
				defaultColor = getResources().getColor(android.R.color.primary_text_light);
			}
			errorTextColor = typedArray.getColor(R.styleable.SupportView_errorTextColor, defaultColor);
		}
		typedArray.recycle();
	}

	@AfterViews
	void afterViews() {
		errorMessage.setTextColor(errorTextColor);
	}

	@UiThread
	public void showProgress() {
		errorPanel.setVisibility(GONE);
		progressView.setVisibility(VISIBLE);
		setVisibility(VISIBLE);
	}

	@UiThread
	public void hide() {
		errorPanel.setVisibility(GONE);
		progressView.setVisibility(GONE);
		setVisibility(GONE);
	}

	@UiThread
	public void showError(String message, OnClickListener onClickListener) {
		progressView.setVisibility(GONE);
		errorMessage.setText(message);
		retryView.setOnClickListener(onClickListener);
		errorPanel.setVisibility(VISIBLE);
		setVisibility(VISIBLE);
	}

	public boolean isLoading() {
		return progressView.getVisibility() == VISIBLE;
	}

}
