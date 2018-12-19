using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.Models.Reply;

namespace TechTalk.Models.Reply
{
    public class PostReplyModel
    {
        public int Id { get; set; }
       
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public bool IsAuthorAdmin { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
         public string PostTitle { get; set; }
         public string PostContent { get; set; }

        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }
        public int ForumId { get; set; }

    }
}