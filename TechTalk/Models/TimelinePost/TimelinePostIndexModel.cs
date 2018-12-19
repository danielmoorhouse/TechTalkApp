using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTalk.Data.Models;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Models.TimelinePost
{
    public class TimelinePostIndexModel
    {
        public IEnumerable<TimelinePostListingModel> TPostList { get; set; }
       // public IEnumerable<TimelinePostReplyModel> PostReplies { get; set; }
       


    }
}