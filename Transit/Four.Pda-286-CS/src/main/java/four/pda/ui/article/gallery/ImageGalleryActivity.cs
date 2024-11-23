using Android.View;
using Android.Widget;
using Androidx.Appcompat.App;
using Androidx.Viewpager.Widget;
using Org.Androidannotations.Annotations;
using Java.Util;
using Four.Pda;
using System.Diagnostics;

namespace Four.Pda.Ui.Article.Gallery
{
    /// <summary>
    /// Created by asavinova on 01/09/16.
    /// </summary>
    public class ImageGalleryActivity : AppCompatActivity
    {
        string currentUrl;
        List<string> images;
        View toolbar;
        ImageView closeView;
        TextView currentIndexView;
        TextView imagesCountView;
        ViewPager pager;
        EventBus eventBus;
        virtual void AfterViews()
        {
            closeView.SetOnClickListener((v) => Finish());
            int index = GetCurrentImageIndex();
            currentIndexView.SetText(String.ValueOf(index + 1));
            imagesCountView.SetText(String.ValueOf(images.Count));
            pager.SetAdapter(new ImagesPagerAdapter(this, images));
            pager.SetCurrentItem(index, true);
            pager.AddOnPageChangeListener(new AnonymousSimpleOnPageChangeListener(this));
        }

        private sealed class AnonymousSimpleOnPageChangeListener : SimpleOnPageChangeListener
        {
            public AnonymousSimpleOnPageChangeListener(ImageGalleryActivity parent)
            {
                this.parent = parent;
            }

            private readonly ImageGalleryActivity parent;
            public void OnPageSelected(int position)
            {
                currentIndexView.SetText(String.ValueOf(position + 1));
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            eventBus.Register(this);
        }

        protected override void OnPause()
        {
            eventBus.Unregister(this);
            base.OnPause();
        }

        public virtual void OnEvent(ImagesPagerAdapter.ImageClickEvent @event)
        {
            toolbar.SetVisibility(toolbar.GetVisibility() == View.INVISIBLE ? View.VISIBLE : View.INVISIBLE);
        }

        public virtual void OnEvent(ImagesPagerAdapter.OutsideImageClickEvent @event)
        {
            Finish();
        }

        private int GetCurrentImageIndex()
        {
            int index = 0;
            foreach (string image in images)
            {
                if (image.Equals(currentUrl))
                {
                    break;
                }

                index++;
            }

            return index;
        }
    }
}