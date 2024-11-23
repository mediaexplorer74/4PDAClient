using Android.Widget;
using Com.Squareup.Picasso;
using Uk.Co.Senab.Photoview;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by asavinova on 10/04/15.
    /// </summary>
    public class Images
    {
        public static void Load(ImageView view, string url)
        {
            Load(view, url, null);
        }

        public static void Load(ImageView view, string url, Callback callback)
        {
            Picasso.With(view.GetContext()).Load(url).Fit().CenterInside().Into(view, callback);
        }

        public static void LoadWithZoom(ImageView view, string url, PhotoViewAttacher attacher)
        {
            Callback callback = new AnonymousCallback(this);
            Load(view, url, callback);
        }

        private sealed class AnonymousCallback : Callback
        {
            public AnonymousCallback(Images parent)
            {
                this.parent = parent;
            }

            private readonly Images parent;
            public void OnSuccess()
            {
                attacher.Update();
            }

            public void OnError()
            {
            }
        }
    }
}