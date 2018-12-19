using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data.Models;

namespace TechTalk.Data
{
    public interface IFollowService
    {
         Task<bool> Follow(string followerName, string followedName);
        Task<bool> Unfollow(string followerName, string followedName);
        Task<bool> IsFollowing(string followerName, string followedName);
        Task<int> FollowerCount(string userName);
        Task<int> FollowingCount(string userName);
        Task<List<ApplicationUser>> GetFollowers(string userName);
        Task<List<ApplicationUser>> GetFollowing(string userName);
    }
}