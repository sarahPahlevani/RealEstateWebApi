using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.Infrastructure
{
    public class BookmarkDto : ModelDtoBase<Bookmark>
    {
        public override int Id { get; set; }

        [Required]
        public int UserAccountId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }


        public override IModelDto<Bookmark> From(Bookmark entity)
        {
            Id = entity.Id;
            UserAccountId = entity.UserAccountId;
            PropertyId = entity.PropertyId;
            DateCreated = entity.DateCreated;
            return this;
        }

        public override Bookmark Create() =>
            new Bookmark
            {
                UserAccountId = UserAccountId,
                PropertyId = PropertyId,
                DateCreated = DateCreated,
            };

        public override Bookmark Update() =>
            new Bookmark
            {
                Id = Id,
                UserAccountId = UserAccountId,
                PropertyId = PropertyId,
                DateCreated = DateCreated,
            };
    }
}
