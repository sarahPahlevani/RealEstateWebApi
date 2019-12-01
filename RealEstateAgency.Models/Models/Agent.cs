using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Agent
    {
        public Agent()
        {
            Request = new HashSet<Request>();
            RequestAction = new HashSet<RequestAction>();
            RequestActionFollowUp = new HashSet<RequestActionFollowUp>();
            RequestAgent = new HashSet<RequestAgent>();
            RequestState = new HashSet<RequestState>();
        }

        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public int UserAccountId { get; set; }
        public string MetadataJson { get; set; }
        public byte[] BusinessCardFront { get; set; }
        public byte[] BusinessCardBack { get; set; }
        public string Description { get; set; }
        public bool IsResponsible { get; set; }
        public bool HasPublishingAuthorization { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual RealEstate RealEstate { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<RequestAction> RequestAction { get; set; }
        public virtual ICollection<RequestActionFollowUp> RequestActionFollowUp { get; set; }
        public virtual ICollection<RequestAgent> RequestAgent { get; set; }
        public virtual ICollection<RequestState> RequestState { get; set; }
    }
}
