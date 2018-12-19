using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.Data.Models;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Models.TimelinePost
{
    public class TimelinePostListingModel
    {
        public int Id { get; set; }
        
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
       
        public string AuthorId { get; set; }

        public string AuthorImageUrl { get; set; }

        public string PostContent { get; set;  }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public int RepliesCount { get; set; }

        public virtual IEnumerable<TimelinePostReplyModel> Replies { get; set; }

        

        
    }
}