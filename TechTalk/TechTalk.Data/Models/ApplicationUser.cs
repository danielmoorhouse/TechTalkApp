using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace TechTalk.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Rating { get; set; }

        public string Location { get; set; }
        public string MyTech { get; set; }
        public string ProfileImageUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }

         public bool IsDeleted { get; set; } = false;
        

         
    }
}