using System;
using System.ComponentModel.DataAnnotations;

namespace TechTalk.Models.TimelinePost
{
    public class NewTimelinePostModel
    {
        public string AuthorId { get; set; }
        public string AuthorImageUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        
        public string AuthorName { get; set; }
        public string Content { get; set; }
    }
}