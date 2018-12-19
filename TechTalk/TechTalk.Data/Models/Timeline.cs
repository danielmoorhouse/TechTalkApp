using System.Collections.Generic;

namespace TechTalk.Data.Models
{
    public class Timeline
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<TimeLinePost> TimeLinePost { get; set; }
    }
}