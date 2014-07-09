using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {
        [Key]    
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public string Message { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Reply> Replies { get; set; }
        public UserProfile Author { get; set; }
        //public bool IsAuthorized { get; set; }

    }
}