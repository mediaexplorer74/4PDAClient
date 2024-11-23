using Android.View;
using Uk.Co.Senab.Photoview;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    /// <summary>
    /// Created by pavel on 11/09/16.
    /// </summary>
    public class DoubleTapListener : DefaultOnDoubleTapListener
    {
        private PhotoViewAttacher attacher;
        public DoubleTapListener(PhotoViewAttacher attacher) : base(attacher)
        {
            this.attacher = attacher;
        }

        public override bool OnDoubleTap(MotionEvent ev)
        {
            if (attacher == null)
                return false;
            try
            {
                float scale = attacher.GetScale();
                float x = ev.GetX();
                float y = ev.GetY();
                if (scale < attacher.GetMediumScale())
                {
                    attacher.SetScale(attacher.GetMediumScale(), x, y, true);
                }
                else
                {
                    attacher.SetScale(attacher.GetMinimumScale(), x, y, true);
                }
            }
            catch (ArrayIndexOutOfBoundsException e)
            {
            }

            return true;
        }
    }
}