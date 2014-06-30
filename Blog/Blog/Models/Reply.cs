using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Blog.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public User Replier { get; set; }


    }
}
