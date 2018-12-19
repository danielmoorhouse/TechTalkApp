using System.ComponentModel.DataAnnotations;
using TechTalk.Models.Forum;

namespace TechTalk.Models.Post
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }

         [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string DatePosted { get; set; }
        //public string ImageUrl { get; set; }

        // public int ForumId { get; set; }
        // public string ForumImageUrl { get; set; }
        // public string ForumName { get; set; }

         public ForumListingModel Forum { get; set; }
        public int RepliesCount { get; set; }
    }    
}