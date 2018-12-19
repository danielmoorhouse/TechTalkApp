using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.Data.Models;

namespace TechTalk.Data
{
    public interface INotificationService
    {
        Notification GetById(int id);
        Task<Notification> CreateNotification(string toUser, string fromUser, string notificationMessage);
        Task SendNotification(Notification notification);
        Task MarkNotificationsViewed(string userName);
        Task<List<Notification>> GetNotifications(string userName);
        //Task<List<Notification>> GetNotificationsByUserId (string userId);
    }
}