using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Data.Models
{
    public class TimeLinePost
    {
          public int Id { get; set; }
        
        public string Content { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }

        public IEnumerable<TimelinePostReply> Replies { get; set; }

    }
}