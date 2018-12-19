using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTalk.Data.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Forum Forum { get; set; }

        public  virtual IEnumerable<PostReply> Replies { get; set; }
    }
}