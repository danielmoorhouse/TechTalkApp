using System;
using System.Threading.Tasks;
using TechTalk.Data;
using Microsoft.EntityFrameworkCore;
using TechTalk.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace TechTalk.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _db;
        public ForumService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Forum forum)
        {
           _db.Add(forum);
           await _db.SaveChangesAsync();
        }

        public async Task Delete(int forumId)
        {
            var forum = GetById(forumId);
            _db.Remove(forum);
           await _db.SaveChangesAsync();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _db.Forums
                .Include(forum => forum.Posts);
        }

        public System.Collections.Generic.IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
           var forum = _db.Forums.Where(f => f.Id == id)
                    .Include(f => f.Posts)
                         .ThenInclude(p => p.User)
                    .Include(f => f.Posts)
                         .ThenInclude(p => p.Replies)
                                .ThenInclude(r => r.User)
                    .FirstOrDefault();     

             return forum;              


        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}