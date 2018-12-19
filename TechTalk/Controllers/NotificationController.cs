using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Data;
using TechTalk.Data.Models;
using TechTalk.Models.Notification;

namespace TechTalk.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly INotificationService _notificationService;
        private readonly IApplicationUser _userService;
     

        public NotificationController(INotificationService notificationService, ApplicationDbContext db, IApplicationUser userService)
        {
            _notificationService = notificationService;
            _db = db;
            _userService = userService;
            
        }
          public int Count()
        {
            var notifications = _notificationService.GetNotifications(User.Identity.Name);
            return notifications.Result.Count;
        }
         [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
       
          var notifications = await _notificationService.GetNotifications(User.Identity.Name);

            await _notificationService.MarkNotificationsViewed(User.Identity.Name);

             return View(notifications);
        }
    }
}