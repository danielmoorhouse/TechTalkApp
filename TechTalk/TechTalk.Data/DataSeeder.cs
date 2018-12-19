using TechTalk.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TechTalk.Data
{
    public class DataSeeder
    {
        //private ApplicationDbContext _db;

    public static async Task Initialize(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<ApplicationRole> roleManager)
    {
        context.Database.EnsureCreated();

        String adminId1 = "";
        String adminId2 = "";

        string role1 = "Site Admin";
        string desc1 = "This is the site administrator role";
        
        string role2 = "Forum Admin";
        string desc2 = "This is the forum admin role";

         string password = "P@$$w0rd";

        if (await roleManager.FindByNameAsync(role1) == null) {
            await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
        }
        if (await roleManager.FindByNameAsync(role2) == null) {
            await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
        }

        if (await userManager.FindByNameAsync("c") == null)
         {
            var user = new ApplicationUser 
            {
                UserName = "cmaz@bfc",
                Email = "cmaz@mail.com",
                FirstName = "Colette",
                LastName = "Mazzola",
                Location = "Bispham",
                MyTech = "HTML, CSS & JavaScript",
                ProfileImageUrl = "/images/users/cmaz.jpg",
                MemberSince = DateTime.Now
               
            
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password);
                await userManager.AddToRoleAsync(user, role1);
            }
            adminId1 = user.Id;
        }

        if (await userManager.FindByNameAsync("d") == null)
         {
            var user = new ApplicationUser 
            {
                UserName = "danmoorhouse80",
                Email = "danmoorhouse80@mail.com",
                FirstName = "Daniel",
                LastName = "Moorhouse",
                Location = "Thornton",
                MyTech = "C#, ASP.NET Core",
                ProfileImageUrl = "/images/users/dan.jpg",
                MemberSince = DateTime.Now
                
                
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
             {
                await userManager.AddPasswordAsync(user, password);
                await userManager.AddToRoleAsync(user, role1);
            }
            adminId2 = user.Id;
        }

        if (await userManager.FindByNameAsync("m") == null) 
        {
            var user = new ApplicationUser 
            {
                UserName = "ezzawaite52",
                Email = "ezwaite@mail.com",
                FirstName = "Eric",
                LastName = "Waite",
                Location = "Cleveleys",
                MyTech = "Windows 95",
                ProfileImageUrl = "/images/users/ez.jpg",
                MemberSince = DateTime.Now
               
              
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password);
                await userManager.AddToRoleAsync(user, role2);
            }
        }

        if (await userManager.FindByNameAsync("d") == null) {
            var user = new ApplicationUser {
                UserName = "nicmo@mail.com",
                Email = "nicmo@mail.com",
                FirstName = "Nicola",
                LastName = "Moorhouse",
                Location = "Thornton",
                MyTech = ".NET Core, Sqlite",
                ProfileImageUrl = "/images/users/nic.jpg",
                MemberSince = DateTime.Now
                
                
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password);
                await userManager.AddToRoleAsync(user, role2);
            }
        }
    }
}
}
