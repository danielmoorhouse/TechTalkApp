using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data;
using TechTalk.Data.Models;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace TechTalk.Service
{
    public class TimelinePostService : ITimelinePost
    {
         private readonly ApplicationDbContext _db;
        //private readonly IFollow _followService;
        public TimelinePostService(ApplicationDbContext db)   //, FollowService followService)
        {
            _db = db;
         // _followService = followService;
        }
        

        public async Task Add(TimeLinePost post)
        {
             _db.Add(post); 
           await _db.SaveChangesAsync();
        }

          public async Task AddReply(TimelinePostReply reply)
        {
            _db.TimelinePostReplies.Add(reply);
            await _db.SaveChangesAsync();

        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new System.NotImplementedException();
        }

  
      

        public IEnumerable<TimeLinePost> GetLatestPosts(int n)
        {
            throw new System.NotImplementedException();
        }

        public TimeLinePost GetById(int id)
        {
               return _db.TimeLinePosts.Where(p => p.Id == id)
                .Include(p => p.User)
                .Include(p => p.Replies)
                 .ThenInclude(r => r.User).First();
                
                    
        }

         public IEnumerable<TimeLinePost> GetAll()
        {
            // var following = _followService.GetFollowing(userId);
            // var posts =  _db.TimeLinePosts.Include(p => p.User)
            // .Include(p => p.Replies)
            // .OrderByDescending(c => c.Created).ToList();
            // return View();
            return _db.TimeLinePosts
            .Include(p => p.User)
            .Include(p => p.Replies)
            .OrderByDescending(c => c.Created)
            .ToList();
           
                  
         }

        public IEnumerable<TimeLinePost> GetByUserId(string id)
        {
            return _db.TimeLinePosts.Where(u => u.User.Id == id).OrderByDescending(c => c.Created).ToList().Take(10);
        }

        // public async Task<List<TimeLinePost>> GetTimelinePosts(string userId)
        // {
        //     var following = await _followService.GetFollowing(userId);
        //     var posts = await _db.TimeLinePosts.OrderByDescending(p => p.Created).ToListAsync();
        //     posts.RemoveAll(p => !following.Contains(p.User) && p.User.Id != userId);

        //     return posts; //Only see posts of users you are following
        // }
    }
}