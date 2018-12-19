using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTalk.Data;
using TechTalk.Data.Models;

namespace TechTalk.Service
{
    public class FollowService : IFollowService
    {
        private readonly ApplicationDbContext _db;
        private readonly IApplicationUser _userService;
        public FollowService(IApplicationUser userService, ApplicationDbContext db)
        {
            _userService = userService;
            _db = db;
        }
        public async Task<bool> Follow(string followerName, string followedName)
        {
            // Check if user is already following
            dynamic following = await IsFollowing(followedName, followedName);
            if (following) return true;

            // Can't follow yourself
            if (followerName == followedName) return false;

            following = new Following
            {
                Accepted = true,
                Timestamp = DateTime.Now,
                UserFollowed = await _userService.GetByUsername(followedName),
                UserFollowing = await _userService.GetByUsername(followerName)
            };

            _db.Following.Add(following);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<int> FollowerCount(string userName)
        {
            return await _db.Following.Where(f => f.UserFollowed.UserName == userName).CountAsync();
        }

        public async Task<int> FollowingCount(string userName)
        {
             return await _db.Following.Where(f => f.UserFollowing.UserName == userName).CountAsync();
        }

        public async Task<List<ApplicationUser>> GetFollowers(string userName)
        {
             return await _db.Following
                .Where(f => f.UserFollowed.UserName == userName)
                .Select(f => f.UserFollowing)
                .ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetFollowing(string userName)
        {
                 return await _db.Following
                .Where(f => f.UserFollowing.UserName == userName)
                .Select(f => f.UserFollowed)
                .ToListAsync();
        }

        public async Task<bool> IsFollowing(string followerName, string followedName)
        {
              var following = await _db.Following.FirstOrDefaultAsync(f =>
                f.UserFollowing.UserName == followerName && f.UserFollowed.UserName == followedName
            );
            return following != null;
        }

        public async Task<bool> Unfollow(string followerName, string followedName)
        {
            
            dynamic following = await IsFollowing(followerName, followedName);
            if (!following) return false;

            _db.Following.RemoveRange(await _db.Following.Where(f =>
                f.UserFollowing.UserName == followerName && f.UserFollowed.UserName == followedName
            ).ToListAsync());

            await _db.SaveChangesAsync();

            return true;
        }
    }
}