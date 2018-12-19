using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechTalk.Data;
using TechTalk.Data.Models;

namespace TechTalk.Service
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _db;
        private readonly IApplicationUser _userService;

        public NotificationService(IApplicationUser userService, ApplicationDbContext db)
        {
            _db = db;
            _userService = userService;
        }
        public async Task<Notification> CreateNotification(string toUser, string fromUser, string notificationMessage)
        {
            var notification = new Notification
            {
                ToUser =  await _userService.GetByUsername(toUser),
                FromUser = await _userService.GetByUsername(fromUser),
                Message = notificationMessage,
                Viewed = false,
                Timestamp = DateTime.Now
            };
            return notification;
        }

        public Notification GetById(int id)
        {
              return _db.Notifications.Where(p => p.Id == id)
                .Include(p => p.FromUser)
                .Include(p => p.ToUser)
                    //.ThenInclude(r => r.User)
                .Include(p => p.Timestamp)
                .Include(p => p.Message)
                .First();
        }

        public  Task<List<Notification>> GetNotifications(string userName)
        {
            
            var notifications =  _db.Notifications.Where(n => n.ToUser.UserName == userName);

            return notifications.ToListAsync();

            //return includeViewed ? notifications.ToListAsync() : notifications.Where(n => !n.Viewed).ToListAsync();
        }

        // public Task<List<Notification>> GetNotificationsByUserId(string userId)
        // {
        //      var notifications =  _db.Notifications.Where(n => n.ToUser.UserName == userId);

        //     return  notifications.ToListAsync();
        // }

        public async Task MarkNotificationsViewed(string userName)
        {
            var notifications = _db.Notifications.Where(n => n.ToUser.UserName == userName).ToList();
            notifications.ForEach(n => n.Viewed = true);

            await _db.SaveChangesAsync();
        }

        public async Task SendNotification(Notification notification)
        {
            
            if (notification == null || notification.FromUser.Id == notification.ToUser.Id) return;

            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
        }
    }
}