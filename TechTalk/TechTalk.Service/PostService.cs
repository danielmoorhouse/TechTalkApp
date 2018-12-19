using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data;
using TechTalk.Data.Models;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace TechTalk.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _db;
        public PostService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Add(Post post)
        {
           _db.Add(post); 
           await _db.SaveChangesAsync();
        }

        public async Task AddReply(PostReply reply)
        {
            _db.PostReplies.Add(reply);
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

        public IEnumerable<Post> GetAll()
        {
            return _db.Posts
                  .Include(p => p.User)
                .Include(p => p.Replies)
                    .ThenInclude(r => r.User)
                .Include(p => p.Forum);
        }

        public Post GetById(int id)
        {
            return _db.Posts.Where(p => p.Id == id)
                .Include(p => p.User)
                .Include(p => p.Replies)
                    .ThenInclude(r => r.User)
                .Include(p => p.Forum)
                .First();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {

           return string.IsNullOrEmpty(searchQuery)
            ? forum.Posts : forum.Posts
                .Where(post => post.Title.Contains(searchQuery)
                || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var normalized = searchQuery.ToLower();

            return GetAll().Where(post => post.Title
                    .ToLower()
                    .Contains(normalized) 
                    || post.Content.ToLower().Contains(normalized));;
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
           return GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsByForum(int id)
        {
            return _db.Forums.Where(f => f.Id == id).First().Posts;
        }
    }
}