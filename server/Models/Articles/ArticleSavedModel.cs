using System;

namespace server.Models.Articles
{
    public class ArticleSaveModel
    {
        public Guid Gd { get; set; }
        public Guid ArticleGd { get; set; }
        public Guid UserGd { get; set; }
        public DateTime DateSaved { get; set; }
    }
}