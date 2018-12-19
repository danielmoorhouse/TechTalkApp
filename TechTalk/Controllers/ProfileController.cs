using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TechTalk.Data.Models;
using TechTalk.Data;
using Microsoft.AspNetCore.Authorization;
using TechTalk.Models.ApplicationUser;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TechTalk.Controllers
{

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IFollowService _followService;

        private readonly ITimelinePost _timelinePostService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(
            UserManager<ApplicationUser> userManager, 
            IApplicationUser userService,
            ITimelinePost timelinePostService,
            IUpload uploadService,
            IConfiguration configuration,
            IFollowService followService)
        {
            _userManager = userManager;
            _userService = userService;
            _timelinePostService = timelinePostService;
            _uploadService = uploadService;
           _configuration = configuration;
           _followService = followService;
        }

        public async Task<ActionResult> Detail(string id)
        {
            var userId = id ?? string.Empty;
              // Don't try load a profile called /profile/
            if (id.ToLower() == "profile" && User.Identity.IsAuthenticated && User.Identity.Name != "profile")
                userId = string.Empty;
            var user = await _userService.GetByUsernameOrId(userId);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var timelinePosts = _timelinePostService.GetByUserId(id);

            var model = new ProfileModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MyTech = user.MyTech,
                Location = user.Location,
                UserRating = user.Rating.ToString(), //try and pass userid after following, passing name isn't working
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                MemberSince = user.MemberSince,
                IsAdmin = userRoles.Contains("Site Admin"),
                Posts = timelinePosts,
                PostCount = timelinePosts.Count(),
                FollowerCount = await _followService.FollowerCount(user.UserName),
                FollowingCount = await _followService.FollowingCount(user.UserName),
                Following = false,
                OwnProfile = false
            };
            if (User.Identity.IsAuthenticated)
            {
                model.Following = await _followService.IsFollowing(User.Identity.Name, user.UserName);
                model.OwnProfile = user.UserName == User.Identity.Name;
            }
            

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            //     //   Connect to an Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //     //get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString, "profile-images");

            //     //     // Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            // Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            // Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On that block blob, Upload our file <-- file uploaded to the cloud
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            // Set the User's profile image to the URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            // Redirect to the user's profile page.
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        [Authorize(Roles = "Site Admin")] //add roles = admin
        public IActionResult Index()
        {
            var profiles = _userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileModel
                {
                    Email = u.Email,
                    UserName = u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    MemberSince = u.MemberSince
                });

            var model = new ProfileListModel
            {
                Profiles = profiles
            };

            return View(model);
        }
     
    }
}