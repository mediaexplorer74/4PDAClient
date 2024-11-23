package four.pda.ui;

import androidx.annotation.StringRes;

import java.util.HashMap;
import java.util.Map;

import four.pda.R;
import four.pda.client.CategoryType;

/**
 * Created by asavinova on 22/02/16.
 */
public class CategoryTitleMap {

	private static final Map<CategoryType, Integer> map = new HashMap<>();

	static {
		map.put(CategoryType.ALL, R.string.category_all);
		map.put(CategoryType.NEWS, R.string.category_news);
		map.put(CategoryType.ARTICLES, R.string.category_articles);
		map.put(CategoryType.REVIEWS, R.string.category_reviews);
		map.put(CategoryType.SOFTWARE, R.string.category_software);
		map.put(CategoryType.GAMES, R.string.category_games);
	}

	@StringRes
	public static int get(CategoryType type) {
		Integer stringId = map.get(type);
		if (stringId == null) {
			throw new IllegalArgumentException("No title for type " + type.name());
		}
		return stringId;
	}

}
