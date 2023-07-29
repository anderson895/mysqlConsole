using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notif_Models
{
    public class NotificationRepository
    {

        public List<Notification> _notifications;

        public NotificationRepository()
        {
            _notifications = new List<Notification>();


        }

        public void SaveNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

    }
}
