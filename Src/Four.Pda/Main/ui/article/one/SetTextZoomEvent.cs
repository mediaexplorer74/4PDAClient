using System.Diagnostics;

namespace Four.Pda.Ui.Article.One
{
    /// <summary>
    /// Created by asavinova on 25/03/16.
    /// </summary>
    public class SetTextZoomEvent
    {
        private int zoom;
        public SetTextZoomEvent(int zoom)
        {
            this.zoom = zoom;
        }

        public virtual int GetZoom()
        {
            return zoom;
        }
    }
}