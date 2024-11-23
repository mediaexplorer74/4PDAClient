using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Widget;
using Org.Slf4j;
using Four.Pda;
using System.Diagnostics;

namespace Four.Pda.Ui
{
    public class AspectRatioImageView : ImageView
    {
        private static readonly Logger L = LoggerFactory.GetLogger(typeof(AspectRatioImageView));
        private float aspectRatio = 1;
        public AspectRatioImageView(Context context) : base(context)
        {
        }

        public AspectRatioImageView(Context context, AttributeSet attrs) : base(context, attrs)
        {
            Init(attrs);
        }

        public AspectRatioImageView(Context context, AttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Init(attrs);
        }

        private void Init(AttributeSet attrs)
        {
            TypedArray typedArray = GetContext().ObtainStyledAttributes(attrs, R.styleable.AspectRatio);
            aspectRatio = typedArray.GetFloat(R.styleable.AspectRatio_aspectRatio, 1);
            typedArray.Recycle();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            SetMeasuredDimension(widthMeasureSpec, (int)(GetMeasuredWidth() * aspectRatio));
        }

        public virtual void SetAspectRatio(float aspectRatio)
        {
            this.aspectRatio = aspectRatio;
            RequestLayout();
        }
    }
}