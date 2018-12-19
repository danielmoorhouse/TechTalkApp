using System;

namespace TechTalk.Models.Message
{
    public class MessageModel
    {
          public string SenderId { get; set; }

        public string MessageText { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsSeen { get; set; }

        public string SenderFullName { get; set; }
    }
}