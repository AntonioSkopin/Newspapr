using System;

namespace server.DTO
{
    public class ArticleDTO
    {
        public Guid Gd { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid PostedBy { get; set; }
        public string Tag { get; set; }
        public string ImageID { get; set; }
        public string Fullname { get; set; }
        public int NumSaved { get; set; }
        public int NumLikes { get; set; }
    }
}