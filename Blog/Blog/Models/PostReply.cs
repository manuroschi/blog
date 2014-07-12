using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PostReply
    {
        public int PostId { get; set; }
        public string Comment { get; set; }
    }
}