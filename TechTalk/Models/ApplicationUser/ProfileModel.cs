using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TechTalk.Data.Models;
using TechTalk.Mapping;

namespace TechTalk.Models.ApplicationUser
{
    public class ProfileModel 
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string MyTech { get; set; }
        public string UserRating { get; set; }
        public string ProfileImageUrl { get; set; }
        public bool IsAdmin { get; set; }

        public int PostCount { get; set; }

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }

        public bool Following { get; set; }

        public bool OwnProfile { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MemberSince { get; set; }
        public virtual IEnumerable<TimeLinePost> Posts { get; set; }
        public virtual IEnumerable<TimelinePostReply> Replies { get; set; }
        public int ReplyCount { get; set; }


    }
}