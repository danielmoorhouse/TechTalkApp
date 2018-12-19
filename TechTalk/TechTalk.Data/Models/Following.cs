using System;

namespace TechTalk.Data.Models
{
    public class Following
    {
          public int Id { get; set; }

        public virtual ApplicationUser UserFollowing { get; set; }

        public virtual ApplicationUser UserFollowed { get; set; }

        public DateTime Timestamp { get; set; }

        public bool Accepted { get; set; }
    }
}