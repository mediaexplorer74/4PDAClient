using Android.Content;
using Org.Androidannotations.Annotations;
using Java.Util;
using Four.Pda.Client;
using System.Diagnostics;

namespace Four.Pda.Analytics
{
    public class Analytics
    {
        private Drawer drawer = new Drawer();
        private ArticlesList articlesList = new ArticlesList();
        private Article article = new Article();
        private Comments comments = new Comments();
        private Search search = new Search();
        private readonly AnalyticsTracker tracker;
        public Analytics(Context context)
        {
            this.tracker = new AnswersProxyTracker(context);
        }

        public virtual Drawer Drawer()
        {
            return drawer;
        }

        public virtual ArticlesList ArticlesList()
        {
            return articlesList;
        }

        public virtual Article Article()
        {
            return article;
        }

        public virtual Comments Comments()
        {
            return comments;
        }

        public virtual Search Search()
        {
            return search;
        }

        public class Drawer
        {
            public virtual void AboutClicked()
            {
                tracker.SendCustomEvent("Drawer.About.Click");
            }

            public virtual void CategoryClicked(CategoryType type)
            {
                tracker.SendCustomEvent("Drawer.Category.Click", new AnonymousHashMap(this));
            }

            private sealed class AnonymousHashMap : HashMap
            {
                public AnonymousHashMap(Drawer parent)
                {
                    this.parent = parent;
                }

                private readonly Drawer parent;
                static AnonymousHashMap()
                {
                    Put("type", type.Name());
                }
            }

            public virtual void FeedbackClicked()
            {
                tracker.SendCustomEvent("Drawer.Feedback.Click");
            }

            public virtual void LoginClicked()
            {
                tracker.SendCustomEvent("Drawer.Login.Click");
            }

            public virtual void LogoutClicked()
            {
                tracker.SendCustomEvent("Drawer.Logout.Click");
            }

            public virtual void ProfileClicked()
            {
                tracker.SendCustomEvent("Drawer.Profile.Click");
            }
        }

        public class ArticlesList
        {
            public virtual void ScrollUp(int currentPosition)
            {
                tracker.SendCustomEvent("ArticleList.ScrollUp", new AnonymousHashMap1(this));
            }

            private sealed class AnonymousHashMap1 : HashMap
            {
                public AnonymousHashMap1(ArticlesList parent)
                {
                    this.parent = parent;
                }

                private readonly ArticlesList parent;
                static AnonymousHashMap1()
                {
                    Put("position", String.ValueOf(currentPosition));
                }
            }

            public virtual void ProfileClicked()
            {
                tracker.SendCustomEvent("ArticleList.Profile.Click");
            }
        }

        public class Article
        {
            public virtual void Open()
            {
                tracker.SendCustomEvent("Article.Open");
            }

            public virtual void ProfileClicked()
            {
                tracker.SendCustomEvent("Article.Profile.Click");
            }

            public virtual void TextZoomOpen()
            {
                tracker.SendCustomEvent("Article.TextZoom.Open");
            }

            public virtual void TextZoomSet(int zoom)
            {
                tracker.SendCustomEvent("Article.TextZoom.Set", new AnonymousHashMap2(this));
            }

            private sealed class AnonymousHashMap2 : HashMap
            {
                public AnonymousHashMap2(Article parent)
                {
                    this.parent = parent;
                }

                private readonly Article parent;
                static AnonymousHashMap2()
                {
                    Put("zoom", String.ValueOf(zoom));
                }
            }

            public virtual void Share()
            {
                tracker.SendCustomEvent("Article.Share");
            }

            public virtual void OpenImageGallery()
            {
                tracker.SendCustomEvent("Article.ImageGallery.Open");
            }
        }

        public class Comments
        {
            public virtual void Open()
            {
                tracker.SendCustomEvent("Comments.Open");
            }

            public virtual void Add()
            {
                tracker.SendCustomEvent("Comments.AddButton.Click");
            }

            public virtual void Reply()
            {
                tracker.SendCustomEvent("Comments.ReplyButton.Click");
            }

            public virtual void ShowDialog()
            {
                tracker.SendCustomEvent("Comments.ActionsDialog.Show");
            }

            public virtual void ProfileClicked()
            {
                tracker.SendCustomEvent("Comments.ActionsDialog.Profile.Click");
            }

            public virtual void Like()
            {
                tracker.SendCustomEvent("Comments.ActionsDialog.Like");
            }

            public virtual void Share()
            {
                tracker.SendCustomEvent("Comments.ActionsDialog.Share");
            }
        }

        public class Search
        {
            public virtual void Open()
            {
                tracker.SendCustomEvent("Search.Open");
            }

            public virtual void ScrollUp(int currentPosition)
            {
                tracker.SendCustomEvent("Search.ScrollUp", new AnonymousHashMap3(this));
            }

            private sealed class AnonymousHashMap3 : HashMap
            {
                public AnonymousHashMap3(Search parent)
                {
                    this.parent = parent;
                }

                private readonly Search parent;
                static AnonymousHashMap3()
                {
                    Put("position", String.ValueOf(currentPosition));
                }
            }

            public virtual void Load(string searchCriteria)
            {
                tracker.Search(searchCriteria);
            }
        }
    }
}