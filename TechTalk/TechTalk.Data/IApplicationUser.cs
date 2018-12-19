using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data.Models;
using System;
using TechTalk.Models;
using TechTalk.Models.ApplicationUser;

namespace TechTalk.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();
         Task<ApplicationUser> GetByUsername(string userName);
         Task<ApplicationUser> GetByUsernameOrId(string userName);

        Task SetProfileImage(string id, Uri uri);
        Task UpdateUserRating(string id, Type type);

         bool UserExists(string userId);
        List<UserListModel> All(int pageIndex, int pageSize);

          
    }
}