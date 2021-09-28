using System;
using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class ArticleLike
    {
        [Key]
        public Guid Gd { get; set; }
        public Guid ArticleGd { get; set; }
        public Guid UserGd { get; set; }
        public DateTime DateLiked { get; set; }
    }
}