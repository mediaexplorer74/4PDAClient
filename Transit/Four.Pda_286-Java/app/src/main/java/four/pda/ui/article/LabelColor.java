package four.pda.ui.article;

import android.graphics.Color;

import com.google.common.collect.ImmutableMap;

import java.util.Map;

/**
 * Color generator (int format) from color name.
 * Names imported from server CSS.
 *
 * @author Pavel Savinov (swapii@gmail.com)
 */
public class LabelColor {

	private static final Map<String, Integer> map = ImmutableMap.<String, Integer>builder()
			.put("orange", color(250, 114, 4, 0.85))
			.put("green", color(52, 114, 4, 0.85))
			.put("blue", color(0, 114, 188, 0.85))
			.put("magenta", color(154, 0, 90, 0.85))
			.build();

	private static final int DEFAULT = color(170, 78, 184, 0.85);

	private static int color(int red, int green, int blue, double alpha) {
		return Color.argb((int) (255 * alpha), red, green, blue);
	}

	public static int getColorValueByName(String colorName) {
		Integer color = map.get(colorName);
		if (color == null) {
			color = DEFAULT;
		}
		return color;
	}

}
