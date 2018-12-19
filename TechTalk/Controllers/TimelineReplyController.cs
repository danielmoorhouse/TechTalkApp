using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Data;
using TechTalk.Data.Models;
using TechTalk.Models.TimelineReply;

namespace TechTalk.Controllers
{
    public class TimelineReplyController : Controller
    {
        private readonly ITimelinePost _timelinePostService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        public TimelineReplyController(IPost postService, UserManager<ApplicationUser> userManager, IApplicationUser userService, ITimelinePost timelinePostService)
        {
            _timelinePostService = timelinePostService;
            _userManager = userManager;
            _userService = userService;
        }
           public async Task<IActionResult> Create(int id)
        {
            var post = _timelinePostService.GetById(id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new TimelinePostReplyModel
            {
                PostContent = post.Content,
            
                PostId = post.Id,

                AuthorId = user.Id,
                AuthorFirstName = user.FirstName, //ToDO change to user.FirstName & LastName
                AuthorLastName = user.LastName,
                AuthorImageUrl = user.ProfileImageUrl,
            
                IsAuthorAdmin = User.IsInRole("Site Admin"),


                Created = DateTime.Now
            };
            return View(model);
        }
           [HttpPost]
        public async Task<IActionResult> AddReply(TimelinePostReplyModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            var reply = BuildReply(model, user);

            await _timelinePostService.AddReply(reply);
           // await _userService.UpdateUserRating(userId, typeof(PostReply));

            return RedirectToAction("Index", "TimelinePost", new { id = model.PostId});
        }

         private TimelinePostReply BuildReply(TimelinePostReplyModel model, ApplicationUser user)
        {
            var post = _timelinePostService.GetById(model.PostId);

            return new TimelinePostReply
            {
                Post = post,
                Content = model.ReplyContent,
                Created = DateTime.Now,
                User = user
            };
        }
    }
}