using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Events.Interfaces;
using RealEstateAgency.NotificationSystem.NotifyMessages;

namespace RealEstateAgency.NotificationSystem
{
    public class NotifyUserEvent : IEvent
    {
        public readonly UserNotifyMessage Message;

        public NotifyUserEvent(UserNotifyMessage message)
        {
            Message = message;
        }
    }
}
