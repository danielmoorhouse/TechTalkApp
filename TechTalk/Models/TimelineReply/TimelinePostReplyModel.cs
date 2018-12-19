using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TechTalk.Models.TimelineReply
{
    public class TimelinePostReplyModel
    {
         public int Id { get; set; }
       
        public string AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
       
        public string AuthorImageUrl { get; set; }
        
        public bool IsAuthorAdmin { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
        
        public string PostContent { get; set; }

         
    }
}