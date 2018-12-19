using System.Threading.Tasks;
using TechTalk.Data;
using TechTalk.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using TechTalk.Models;
using Microsoft.EntityFrameworkCore;

namespace TechTalk.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _db;
        

        public ApplicationUserService(ApplicationDbContext db)
        {
            _db = db;
            
        }

    
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll().FirstOrDefault(
                user => user.Id == id);
        }
        public async Task<ApplicationUser> GetByUsernameOrId(string userName)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.Id == userName);
        }



        public async Task SetProfileImage(string id, Uri uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri.AbsoluteUri;
            _db.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserRating(string userId, Type type)
        {
            var user = GetById(userId);
           user.Rating = CalculateUserRating(type, user.Rating);
            await _db.SaveChangesAsync();
        }

        public bool UserExists(string userId) => this._db.Users.Any(u => u.Id == userId && u.IsDeleted == false);
        

        private int CalculateUserRating(Type type, int userRating)
        {
            var inc = 0;

            if (type == typeof(Post))
                inc = 1;

            if (type == typeof(PostReply))
                inc = 3;

            return userRating + inc;
        }

        public List<UserListModel> All(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }


        public async Task<ApplicationUser> GetByUsername(string userName)
        {
             return GetAll().FirstOrDefault(
                 user => user.UserName == userName);
        }



        // public async Task SetProfileImage(string id, Uri uri)
        // {
        //     var user = GetById(id);
        //     user.ProfileImageUrl = uri.AbsoluteUri;
        //     _db.Update(user);
        //     await _db.SaveChangesAsync();
        // }
    }
}
