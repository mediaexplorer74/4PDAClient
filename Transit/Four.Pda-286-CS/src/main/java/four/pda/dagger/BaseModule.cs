using Android.Content;
using Dagger;
using Four.Pda;
using Four.Pda.Ui;
using System.Diagnostics;

namespace Four.Pda.Dagger
{
    /// <summary>
    /// Created by pavel on 21/05/16.
    /// </summary>
    public abstract class BaseModule
    {
        protected Context context;
        public BaseModule(Context context)
        {
            this.context = context;
        }

        public virtual Keyboard Keyboard()
        {
            return new Keyboard(context);
        }

        public virtual Preferences_ Preferences()
        {
            return new Preferences_(context);
        }

        public virtual EventBus EventBus()
        {
            return EventBus_.GetInstance_(context);
        }

        public virtual Auth Auth(Preferences_ preferences, EventBus eventBus)
        {
            return new Auth(preferences, eventBus);
        }
    }
}