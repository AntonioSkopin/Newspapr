using System;

namespace server.Models.Articles
{
    public class ArticleLikeModel
    {
        public Guid Gd { get; set; }
        public Guid ArticleGd { get; set; }
        public Guid UserGd { get; set; }
        public DateTime DateLiked { get; set; }
    }
}