using System;
using System.ComponentModel.DataAnnotations;

namespace server.Entities
{
    public class Article
    {
        [Key]
        public Guid Gd { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid PostedBy { get; set; }
        public DateTime DatePosted { get; set; }
    }
}