using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Models.Post;
using TechTalk.Models.Reply;
using TechTalk.Data.Models;
using TechTalk.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TechTalk.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;

        private static UserManager<ApplicationUser> _userManager;
        public PostController(IPost postService, IForum forumService, 
        UserManager<ApplicationUser> userManager, IApplicationUser userService)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            _userService = userService;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var reply = BuildPostReplies(post.Replies);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content,
                Replies = reply,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title,
                IsAuthorAdmin = IsAuthorAdmin(post.User)

            };

            return View(model);
        }


        public IActionResult Create(int id)
        {
            //id is forum id
            var forum =_forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var post = BuildPost(model, user);

           await _postService.Add(post);
           await _userService.UpdateUserRating(userId, typeof(Post));

            //TODO: Implement User Rating Management

           return RedirectToAction("Index", "Post", new { id =post.Id});
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user,
                Forum = forum 
                
            };
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(r => new PostReplyModel
            {
                Id = r.Id,
                AuthorName = r.User.UserName,
                AuthorId = r.User.Id,
                AuthorImageUrl = r.User.ProfileImageUrl,
                AuthorRating = r.User.Rating,
                Created = r.Created,
                ReplyContent = r.Content,
                IsAuthorAdmin = IsAuthorAdmin(r.User)
             });

        }
    }
}