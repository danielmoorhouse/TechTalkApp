using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Models.TimelinePost
{
    public class TPostIndexModel
    {
         public int Id { get; set; }
        
        public string AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorImageUrl { get; set; }
       

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public string PostContent { get; set; }
        public bool IsAuthorAdmin { get; set; }

        //public int ForumId { get; set; }
       // public string ForumName { get; set; }
        public IEnumerable<TimelinePostReplyModel> Replies { get; set; }
        public int RepliesCount { get; set; }
    }
}