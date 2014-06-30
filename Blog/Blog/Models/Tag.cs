using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Blog.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        
    }

}
