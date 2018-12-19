using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTalk.Data.Models
{
    public class TimelinePostReply
    {
         public int Id { get; set; }
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set ;}
        public  virtual TimeLinePost Post { get; set; }
    }
}