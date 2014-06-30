using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {
        [Key]    
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public List<Tag> Tags { get; set; }
        public Reply Replies { get; set; }
        public User Author { get; set; }
    }
}