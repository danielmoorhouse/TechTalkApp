using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechTalk.Data;
//using Microsoft.AspNetCore.Identity;

namespace TechTalk.Controllers
{
    public class FollowController : Controller
    {
      
        private readonly IApplicationUser _userService;
        private readonly IFollowService _followService;
        private readonly INotificationService _notificationService;

          public FollowController(IApplicationUser userService,
                                IFollowService followService,
                                INotificationService notificationService)
        {
            _userService = userService;
            _followService = followService;
            _notificationService = notificationService;
        }
    
          public async Task<ActionResult> Follow(string userName)
        {
            var user =  _userService.GetByUsername(userName);
            await _followService.Follow(User.Identity.Name, userName);

            var notification = await _notificationService.CreateNotification(userName, User.Identity.Name, string.Format("{0} has followed you.", User.Identity.Name));
            await _notificationService.SendNotification(notification);

            return RedirectToAction("Detail", "Profile", new { id = userName });
        }
         public async Task<ActionResult> Unfollow(string userName)
        {
            await _followService.Unfollow(User.Identity.Name, userName);
            return RedirectToAction("Details", "Profile", new { id = userName });
        }
    }
}