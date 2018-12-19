using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data.Models;

namespace TechTalk.Data
{
    public interface ITimelinePost
    {
        TimeLinePost GetById(int id);

        IEnumerable<TimeLinePost> GetByUserId(string id);
        
        IEnumerable<TimeLinePost> GetAll();

        //Task<List<TimeLinePost>> GetTimelinePosts(string userId);
        
        IEnumerable<TimeLinePost> GetLatestPosts(int n);

        Task Add(TimeLinePost post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);
        Task AddReply(TimelinePostReply reply);
        
    }
}