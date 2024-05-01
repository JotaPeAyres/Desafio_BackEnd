using MotorcycleRental.Bussiness.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Bussiness.Interfaces
{
    public interface INotifier
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
    }
}
