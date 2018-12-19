using System.Collections.Generic;
using TechTalk.Models.Post;

namespace TechTalk.Models.Home
{
    public class HomeIndexModel
    {   
        public string SearchQuery { get; set; }
         public IEnumerable<PostListingModel> LatestPosts { get; set; }  

    }
}