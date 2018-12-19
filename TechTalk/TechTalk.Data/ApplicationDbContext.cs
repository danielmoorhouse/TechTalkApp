using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechTalk.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace TechTalk.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{   
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Forum> Forums { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReply> PostReplies { get; set; }
       
         public DbSet<TimeLinePost> TimeLinePosts { get; set; }
         public DbSet<TimelinePostReply> TimelinePostReplies { get; set; }
         public DbSet<Following> Following { get; set; }
         public DbSet<Notification> Notifications { get; set; }
      
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./techtalk.db");
         }
         protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

      
    }
    }     
}
