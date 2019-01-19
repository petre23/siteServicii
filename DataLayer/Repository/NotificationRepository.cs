using DataLayer.Models;
using ServiciiAuto.DataLayer.Models;
using ServiciiAuto.DataLayer.Repository;
using System;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public class NotificationRepository
    {
        public List<Notification> GetNotifications()
        {
            var records = new RecordRepository().GetAllRecords(new FilterModel(),99999,0);
            var notifications = new List<Notification>();

            foreach (var record in records)
            {
                var notification = CreateNotificationForRecord(record);
                if (notification != null)
                {
                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        private Notification CreateNotificationForRecord(Record record)
        {
            var notification = new Notification();
            var recordLink = "/Records/EditRecord?recordId={0}";
            var message = "{0} clientului {1} cu numarul de inmatriculare: {2} va expira pe data de: {3}";

            if (!string.IsNullOrEmpty(record.RecordTypeName) && record.RecordTypeName == "ASIG")
            {
                if (record.ExpirationDate <= DateTime.Now)
                {
                    notification.NotificationDate = record.CreationDate;
                    notification.NotificationLink = string.Format(recordLink, record.Id);
                    notification.NotificationMessage = string.Format(message, "Asigurarea", record.ClientName, record.CarRegistartionNumber, record.ExpirationDateString);
                    notification.NotificationType = 0;
                }
                else if (record.ExpirationDate <= DateTime.Now.AddDays(5))
                {
                    notification.NotificationDate = record.CreationDate;
                    notification.NotificationLink = string.Format(recordLink, record.Id);
                    notification.NotificationMessage = string.Format(message, "Asigurarea", record.ClientName, record.CarRegistartionNumber, record.ExpirationDateString);
                    notification.NotificationType = 1;
                }
            }
            else if (!string.IsNullOrEmpty(record.RecordTypeName) && record.RecordTypeName == "ITP")
            {
                if (record.ExpirationDate <= DateTime.Now)
                {
                    notification.NotificationDate = record.CreationDate;
                    notification.NotificationLink = string.Format(recordLink, record.Id);
                    notification.NotificationMessage = string.Format(message, "ITP-ul", record.ClientName, record.CarRegistartionNumber, record.ExpirationDateString);
                    notification.NotificationType = 0;
                }
                else if (record.ExpirationDate <= DateTime.Now.AddDays(10))
                {
                    notification.NotificationDate = record.CreationDate;
                    notification.NotificationLink = string.Format(recordLink, record.Id);
                    notification.NotificationMessage = string.Format(message, "ITP-ul", record.ClientName, record.CarRegistartionNumber, record.ExpirationDateString);
                    notification.NotificationType = 1;
                }
            }

            return string.IsNullOrEmpty(notification.NotificationMessage) ? null: notification;
        }
    }
}
