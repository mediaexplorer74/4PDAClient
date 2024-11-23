using Java.Io;
using Java.Util;
using Four.Pda.Client;
using Four.Pda.Client.Exceptions;
using Four.Pda.Client.Model;
using Okhttp3;
using System.Diagnostics;

namespace Four.Pda.Dagger
{
    /// <summary>
    /// Created by asavinova on 23/02/16.
    /// </summary>
    public class DummyFourPdaClient : FourPdaClient
    {
        private static IList<ListArticle> articles = new List();
        private static IList<AbstractComment> comments = new List();
        static DummyFourPdaClient()
        {
            AddArticle(1, "Ошибка парсинга", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
            AddArticle(2, "Ошибка сети", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
            AddArticle(3, "Ошибка парсинга комментариев", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");
            AddArticle(4, "Ошибка сети при запросе комментариев", "http://s.4pda.to/Af3e6vSAQSlqjknILz0pcMd4igtNNtXXYZLvV.jpg");

            // http://4pda.ru/2016/03/26/286724/
            AddArticle(286724, "Honor 7 получает обновление до Android Marshmallow", "http://s.4pda.to/2RBAcCaBdc1x4jkcsl7mWqZlpBepS7dfWN5z0.jpg");

            // http://4pda.ru/2016/03/25/286355/
            AddArticle(286355, "Microsoft работает над универсальным приложением Skype", "http://s.4pda.to/2RBAdJQfItAff0ZxZkac7eLOd9jwhpr4Dw8W.jpg");

            // http://4pda.ru/2016/03/25/286425/
            AddArticle(286425, "Выиграй 1 ТБ и антивирус на все гаджеты в конкурсе от Dr. Web", "http://s.4pda.to/2RBAdFIaF2xopvCJdarD5jSlphuRODs2224z1.jpg");
            AddComment("Ortisiz", 0, "Ну вот как раз геймпад и куплю)", 1, Comment.CanLike.ALREADY_LIKED);
            AddDeletedComment(0);
            AddComment("ibis_87", 0, "Не то чтобы я считал, что игра никакая, она, судя по всему, хорошая, но зачем такая сложность искусственная нужна, решительно не понимаю.", 0, Comment.CanLike.CAN);
            AddComment("Dr_Destroi", 1, "ibis_87, \nЗдесь весь кайф в том что ты моешь пройти игру не совершенствуя своего персонажа а совершенствуя свои знания о тактике, ловушек и мув сете врагов! Это уже доказано игроками которые прошли игры серии Souls без прокачки своих персов!", 12, Comment.CanLike.CAN);
            AddComment("ibis_87", 2, "Dr_Destroi, \nЭто я понимаю, потому и говорю, что игра, судя по всему, хорошая. Я только не вижу в этом никакого интереса. Вот приходите вы домой после работы, устали, поели, поговорили с женой, потаскали на руках ребенка. И садитесь часик...поумирать? Позапоминать каждый поворот каждого подземелья? Я понимаю, в чем концепция игры, но непонимаю, как это может приносить удовольствия тем, у кого нет вагона времени играть в неуставшем состоянии.", 123, Comment.CanLike.CAN);
            AddComment("tapdroid", 3, "ibis_87, удовольствие? я бы сказал - играть интересно.", 0, Comment.CanLike.CAN);
            AddComment("ibis_87", 4, "Вот я и не понимаю удовольствия в этом. Если у меня после 10 часов на работе будет гореть стул, то еще до победы над боссом геймпад окажется в телевизоре, а диск - на барахолке. ", 0, Comment.CanLike.CAN);
            AddDeletedComment(5);
            AddComment("Very long nick name that I can imagine in the world", 6, "tapdroid, \nА зачем еще играть?", 0, Comment.CanLike.CAN);
            AddComment("Dr_Destroi", 7, "Тогда игра станет такой же как и все а не одной из миллиона!", 0, Comment.CanLike.CAN);
            AddComment("ibis_87", 8, "Dr_Destroi, \nИз-за того, что к тому, чем она УЖЕ является, не отрезая НИЧЕГО, добавят что-то еще?", 0, Comment.CanLike.CAN);
            AddDeletedComment(0);
            AddDeletedComment(0);
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        private static void AddArticle(int id, string title, string image)
        {
            ListArticle article = new ListArticle();
            article.SetId(id);
            article.SetTitle(title);
            article.SetDescription(title);
            article.SetImage(image);
            article.SetDate(new Date());
            article.SetPublishedDate(new Date());
            article.SetCommentsCount(comments.Count);
            article.SetAuthor(GetUser("Test"));
            articles.Add(article);
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        private static void AddComment(string nick, int level, string content, int likesCount, Comment.CanLike canLike)
        {
            Comment comment = new Comment();
            comment.SetId(NewId());
            comment.SetDate(new Date());
            comment.SetUser(GetUser(nick));
            comment.SetLevel(level);
            comment.SetContent(content);
            comment.SetCanReply(level < 8);
            Comment.Karma karma = new Karma();
            karma.SetLikesCount(likesCount);
            karma.SetCanLike(canLike);
            karma.SetUnknown2(false);
            karma.SetUnknown3(0);
            comment.SetKarma(karma);
            comments.Add(comment);
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        private static User GetUser(string nick)
        {
            User user = new User();
            user.SetId(1);
            user.SetNickname(nick);
            return user;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        private static void AddDeletedComment(int level)
        {
            DeletedComment comment = new DeletedComment();
            comment.SetId(NewId());
            comment.SetLevel(level);
            comment.SetContent("Комментарий удален");
            comment.SetCanReply(false);
            comments.Add(comment);
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public DummyFourPdaClient(OkHttpClient client) : base(client)
        {
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        private static long NewId()
        {
            return (long)(Math.Random() * Long.MAX_VALUE);
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override IList<ListArticle> GetArticles(CategoryType type, int page)
        {
            return articles;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override ArticleContent GetArticleContent(Date date, long id)
        {
            if (id == 1)
            {
                throw new ParseException("");
            }

            if (id == 2)
            {
                throw new IOException();
            }

            foreach (ListArticle article in articles)
            {
                if (article.GetId() == id)
                {
                    ArticleContent articleContent = new ArticleContent();
                    articleContent.SetContent(article.GetDescription());
                    articleContent.SetImages(new List<string>());
                    return articleContent;
                }
            }

            return null;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override CommentsContainer GetArticleComments(Date date, long id)
        {
            if (id == 3)
            {
                throw new ParseException("");
            }

            if (id == 4)
            {
                throw new IOException();
            }

            CommentsContainer container = new CommentsContainer();
            container.SetComments(comments);
            container.SetCanAddNewComment(true);
            return container;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override CommentsContainer AddComment(long postId, long replyId, string message)
        {
            CommentsContainer container = new CommentsContainer();
            container.SetCanAddNewComment(true);
            Comment comment = new Comment();
            comment.SetId(System.CurrentTimeMillis());
            comment.SetDate(new Date());
            comment.SetUser(GetUser("You"));
            comment.SetContent(message);
            if (replyId == null)
            {
                comment.SetLevel(0);
                comments.Add(comment);
                container.SetComments(comments);
                return container;
            }

            IList<AbstractComment> updatedComments = new List();
            foreach (AbstractComment cmnt in comments)
            {
                updatedComments.Add(cmnt);
                if (cmnt.GetId() == replyId)
                {
                    comment.SetLevel(cmnt.GetLevel() + 1);
                    updatedComments.Add(comment);
                }
            }

            comments = updatedComments;
            container.SetComments(comments);
            return container;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override long Login(LoginParams @params)
        {
            return 4975039;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override bool Logout()
        {
            return true;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override Profile GetProfile(long id)
        {
            Profile profile = new Profile();
            profile.SetLogin("var.ann");
            profile.SetPhoto("http://s.4pda.to/tp6nuQlKPdPSv8fwz1HfNVeHMOUxPbaFg.jpg");
            profile.SetInfo("User info");
            return profile;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override SearchContainer SearchArticles(string search, int page)
        {
            SearchContainer container = new SearchContainer();
            container.SetAllArticlesCount(articles.Count);
            container.SetHasNextPage(false);
            IList<SearchListArticle> searchArticles = new List();
            double position = 0;
            foreach (ListArticle article in articles)
            {
                SearchListArticle searchArticle = new SearchListArticle();
                searchArticle.SetId(article.GetId());
                searchArticle.SetDate(article.GetDate());
                searchArticle.SetTitle(article.GetTitle());
                searchArticle.SetDescription(article.GetDescription());
                searchArticle.SetImage(article.GetImage());
                searchArticle.SetPosition(position);
                searchArticle.SetAuthor(article.GetAuthor());
                searchArticles.Add(searchArticle);
                position++;
            }

            container.SetArticles(searchArticles);
            return container;
        }

        // http://4pda.ru/2016/03/26/286724/
        // http://4pda.ru/2016/03/25/286355/
        // http://4pda.ru/2016/03/25/286425/
        public override void LikeArticleComment(long articleId, long commentId)
        {
            foreach (AbstractComment comment in comments)
            {
                if (comment.GetId() == commentId)
                {
                    Comment.Karma karma = ((Comment)comment).GetKarma();
                    karma.SetLikesCount(karma.GetLikesCount() + 1);
                    karma.SetCanLike(Comment.CanLike.ALREADY_LIKED);
                    ((Comment)comment).SetKarma(karma);
                    return;
                }
            }
        }
    }
}