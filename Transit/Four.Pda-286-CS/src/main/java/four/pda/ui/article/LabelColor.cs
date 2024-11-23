using Android.Graphics;
using Com.Google.Common.Collect;
using Java.Util;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    public class LabelColor
    {
        private static readonly Dictionary<string, int> map = ImmutableMap.Builder().Put("orange", Color(250, 114, 4, 0.85)).Put("green", Color(52, 114, 4, 0.85)).Put("blue", Color(0, 114, 188, 0.85)).Put("magenta", Color(154, 0, 90, 0.85)).Build();
        private static readonly int DEFAULT = Color(170, 78, 184, 0.85);
        private static int Color(int red, int green, int blue, double alpha)
        {
            return Color.Argb((int)(255 * alpha), red, green, blue);
        }

        public static int GetColorValueByName(string colorName)
        {
            int color = map[colorName];
            if (color == null)
            {
                color = DEFAULT;
            }

            return color;
        }
    }
}