namespace TechTalk.Models.Notification
{
    public class NotificationIndexModel
    {
        public int Id { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Created { get; set; }
        public string Message { get; set; }
    }
}