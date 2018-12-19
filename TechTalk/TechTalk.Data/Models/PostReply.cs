using System;
using System.ComponentModel.DataAnnotations;

namespace TechTalk.Data.Models
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set ;}
        public virtual Post Post { get; set; }
    }
}