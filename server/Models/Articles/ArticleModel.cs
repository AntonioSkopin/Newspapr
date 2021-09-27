using System;

namespace server.Models.Articles
{
    public class ArticleModel
    {
        public Guid Gd { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid PostedBy { get; set; }
        public string Tag { get; set; }
        public string ImageID { get; set; }
    }
}