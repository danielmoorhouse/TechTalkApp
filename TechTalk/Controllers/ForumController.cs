using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Data;
using TechTalk.Models.Forum;
using TechTalk.Models.Post;
using TechTalk.Data.Models;
using TechTalk.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.IO;

namespace TechTalk.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
         private readonly IPost _postService;
         private readonly IUpload _uploadService;
         private readonly IConfiguration _configuration;

        public ForumController(IForum forumService,
         IPost postService,
          IUpload uploadService,
            IConfiguration configuration)
        {
            _forumService = forumService;
            _postService = postService;
            _uploadService = uploadService;
           _configuration = configuration;
        }

        public IActionResult Index()
        {
            var forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel 
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                });
                var model = new ForumIndexModel
                {
                    ForumList = forums
                };
            return View(model);
        }
     
        public IActionResult Topic(int id, string searchQuery)
        {
            var forum = _forumService.GetById(id);
            var posts = new List<Post>();

           posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();
        
           
            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                AuthorName = post.User.UserName,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }
        public IActionResult Create()
        {
            var model = new AddForumModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddForum(AddForumModel model)
        {
            var imageUri = "/images/users/default.png";

            if(model.ImageUpload != null)
            {
                var blockBlob = UploadForumImage(model.ImageUpload);
                imageUri = blockBlob.Uri.AbsoluteUri;
            }
            var forum = new Forum
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.Now,
                ImageUrl = imageUri

            };
            await _forumService.Create(forum);
            return RedirectToAction("Index", "Forum");
        }

        private CloudBlockBlob UploadForumImage(IFormFile file)
        {
                //Connect to an Azure Storage Account Container
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");

            //get Blob Container
            var container = _uploadService.GetBlobContainer(connectionString, "");

            // Parse the Content Disposition response header
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            // Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            // Get a reference to a Block Blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On that block blob, Upload our file <-- file uploaded to the cloud
            blockBlob.UploadFromStreamAsync(file.OpenReadStream());
            
            
            return blockBlob;
        
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;
            return BuildForumListing(forum);


        }
    
    private ForumListingModel BuildForumListing(Forum forum)
    {
       return new ForumListingModel
        {
            Id = forum.Id,
            Name = forum.Title,
            Description = forum.Description,
            ImageUrl = forum.ImageUrl
        };
    }
}
}