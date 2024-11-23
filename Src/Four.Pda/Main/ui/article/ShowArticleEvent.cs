using Java.Util;
using System.Diagnostics;

namespace Four.Pda.Ui.Article
{
    /// <summary>
    /// Created by asavinova on 13/04/15.
    /// </summary>
    public class ShowArticleEvent
    {
        private long id;
        private Date date;
        private string title;
        private string image;
        private long authorId;
        private string authorName;
        private readonly string labelName;
        private readonly string labelColor;
        public ShowArticleEvent(long id, Date date, string title, string image, long authorId, string authorName, string labelName, string labelColor)
        {
            this.id = id;
            this.date = date;
            this.title = title;
            this.image = image;
            this.authorId = authorId;
            this.authorName = authorName;
            this.labelName = labelName;
            this.labelColor = labelColor;
        }

        public virtual long GetId()
        {
            return id;
        }

        public virtual Date GetDate()
        {
            return date;
        }

        public virtual string GetTitle()
        {
            return title;
        }

        public virtual string GetImage()
        {
            return image;
        }

        public virtual long GetAuthorId()
        {
            return authorId;
        }

        public virtual string GetAuthorName()
        {
            return authorName;
        }

        public virtual string GetLabelName()
        {
            return labelName;
        }

        public virtual string GetLabelColor()
        {
            return labelColor;
        }
    }
}