using Android.Content;
using Android.View;
using Android.Widget;
using Androidx.Annotation;
using Androidx.Viewpager.Widget;
using Java.Util;
using Four.Pda;
using Four.Pda.Ui;
using Uk.Co.Senab.Photoview;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Gallery
{
    /// <summary>
    /// Created by asavinova on 01/09/16.
    /// </summary>
    public class ImagesPagerAdapter : PagerAdapter
    {
        private Context context;
        private IList<string> images = new List();
        public ImagesPagerAdapter(Context context, IList<string> images)
        {
            this.context = context;
            this.images = images;
        }

        public override int GetCount()
        {
            return images.Count;
        }

        public override bool IsViewFromObject(View view, object @object)
        {
            return view == @object;
        }

        public override object InstantiateItem(ViewGroup collection, int position)
        {
            ImageView imageView = new ImageView(context);
            Images.LoadWithZoom(imageView, images[position], GetPhotoViewAttacher(imageView));
            collection.AddView(imageView);
            return imageView;
        }

        private PhotoViewAttacher GetPhotoViewAttacher(ImageView imageView)
        {
            PhotoViewAttacher attacher = new PhotoViewAttacher(imageView);
            attacher.SetScaleLevels(1, 3, 8);
            attacher.SetOnDoubleTapListener(new DoubleTapListener(attacher));
            attacher.SetOnPhotoTapListener(new AnonymousOnPhotoTapListener(this));
            return attacher;
        }

        private sealed class AnonymousOnPhotoTapListener : OnPhotoTapListener
        {
            public AnonymousOnPhotoTapListener(ImagesPagerAdapter parent)
            {
                this.parent = parent;
            }

            private readonly ImagesPagerAdapter parent;
            public void OnPhotoTap(View view, float x, float y)
            {
                EventBus_.GetInstance_(view.GetContext()).Post(new ImageClickEvent());
            }

            public void OnOutsidePhotoTap()
            {
                EventBus_.GetInstance_(context).Post(new OutsideImageClickEvent());
            }
        }

        public override void DestroyItem(ViewGroup collection, int position, object view)
        {
            collection.RemoveView((View)view);
        }

        public class ImageClickEvent
        {
        }

        public class OutsideImageClickEvent
        {
        }
    }
}