using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel.Events.Interfaces;
using RealEstateAgency.Controllers.Crm;
using RealEstateAgency.Dtos.ModelDtos.Crm;

namespace RealEstateAgency.NotificationSystem.NotifyMessages
{
    public enum UserEventType
    {
        Todo,
        Snooze
    }

    public class UserNotifyMessage : RequestActionFollowUpListDto , IEvent
    {
        public UserEventType Type { get; set; }
        public string ActionTypeName { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public int AgentId { get; set; }
        public int RequestId { get; set; }
        public string Fullname { get; set; }
        public string Firstname { get; set; }
        public string RequestUrl { get; set; }
        public string BaseUrl { get; set; }
    }
}
