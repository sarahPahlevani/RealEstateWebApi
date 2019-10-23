using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Organization
{
    public class AgentListDto : IDto
    {
        public int Id { get; set; }
        public int RealEstateId { get; set; }
        public int UserAccountId { get; set; }
        public string Description { get; set; }
        public bool IsResponsible { get; set; }
        public bool HasPublishingAuthorization { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
