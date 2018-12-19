using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Data;
using TechTalk.Data.Models;
using TechTalk.Models.TimelinePost;
using Microsoft.EntityFrameworkCore;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Controllers
{
    public class TimelinePostController : Controller
    {
         private readonly ITimelinePost _timelinePostService;
        
        private readonly IApplicationUser _userService;

        private static UserManager<ApplicationUser> _userManager;
        public TimelinePostController(ITimelinePost timelinePostService, 
        UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _timelinePostService = timelinePostService;
            _userManager = userManager;
            _userService = userService;
        }
         public IActionResult Index(int id)
        {   
        
            var post = _timelinePostService.GetById(id);
            var postReplies = BuildTimelinePostReplies(post.Replies);
           

            var model = new TPostIndexModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorFirstName = post.User.FirstName,
                AuthorLastName = post.User.LastName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                Created = post.Created,
                PostContent = post.Content,
                RepliesCount = post.Replies.Count(),
                Replies = postReplies,
                IsAuthorAdmin = IsAuthorAdmin(post.User)

            };

            return View(model);
        }

       

        public IActionResult Timeline(string id, TimelinePostReply reply) 
        {
           
            var posts = _timelinePostService.GetAll()
                 .Select(post => new TimelinePostListingModel 
                {
                    Id = post.Id,
                    AuthorId = post.User.Id,
                    AuthorFirstName = post.User.FirstName,
                    AuthorLastName = post.User.LastName,
                    AuthorImageUrl = post.User.ProfileImageUrl,
                    PostContent = post.Content,
                    DatePosted = post.Created,
                    RepliesCount = post.Replies.Count()
                   
                
                    
                });
                var model = new TimelinePostIndexModel
                {
                    TPostList = posts
                    
               };
            return View(model);
     
        }

      

        public async Task<IActionResult> Create(int id)
        {
          
             var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new NewTimelinePostModel
            {
                AuthorId = user.Id,
                AuthorImageUrl = user.ProfileImageUrl,
                AuthorName = User.Identity.Name,
             
                
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(NewTimelinePostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildTimelinePost(model, user);

           await _timelinePostService.Add(post);
         
            
           

           return RedirectToAction("Timeline", "TimelinePost", new { id =post.Id});
        }
          private TimeLinePost BuildTimelinePost(NewTimelinePostModel model, ApplicationUser user)
        {
            
            return new TimeLinePost
            {
                
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                
                
            };
        }
       private IEnumerable<TimelinePostReplyModel> BuildTimelinePostReplies(IEnumerable<TimelinePostReply> replies)
        {
            if (replies == null)
            {
                throw new ArgumentNullException(nameof(replies));
            }

            return replies.Select(r => new TimelinePostReplyModel
            {
                Id = r.Id,
                AuthorFirstName = r.User.FirstName,
                AuthorLastName = r.User.LastName,
                AuthorId = r.User.Id,
                AuthorImageUrl = r.User.ProfileImageUrl,
                Created = r.Created,
                ReplyContent = r.Content,
                IsAuthorAdmin = IsAuthorAdmin(r.User)
             });

        }
        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Site Admin");
        }
      
    }
}