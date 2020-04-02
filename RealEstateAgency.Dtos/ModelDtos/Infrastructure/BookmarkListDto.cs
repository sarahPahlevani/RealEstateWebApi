using System;
using System.Collections.Generic;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class BookmarkListDto : IDto
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
