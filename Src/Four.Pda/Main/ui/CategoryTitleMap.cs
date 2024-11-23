using Androidx.Annotation;
using Java.Util;
using Four.Pda;
using Four.Pda.Client;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by asavinova on 22/02/16.
    /// </summary>
    public class CategoryTitleMap
    {
        private static readonly Dictionary<CategoryType, int> map = new HashMap();
        static CategoryTitleMap()
        {
            map.Put(CategoryType.ALL, R.@string.category_all);
            map.Put(CategoryType.NEWS, R.@string.category_news);
            map.Put(CategoryType.ARTICLES, R.@string.category_articles);
            map.Put(CategoryType.REVIEWS, R.@string.category_reviews);
            map.Put(CategoryType.SOFTWARE, R.@string.category_software);
            map.Put(CategoryType.GAMES, R.@string.category_games);
        }

        public static int Get(CategoryType type)
        {
            int stringId = map[type];
            if (stringId == null)
            {
                throw new ArgumentException("No title for type " + type.Name());
            }

            return stringId;
        }
    }
}