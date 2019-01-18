using System;

namespace DataLayer.Models
{
    public class Notification
    {
        public string NotificationMessage { get; set; }
        public int NotificationType { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationLink { get; set; }
    }
}
