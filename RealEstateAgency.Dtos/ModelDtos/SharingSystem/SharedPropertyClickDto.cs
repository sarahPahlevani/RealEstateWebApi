using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.SharingSystem
{
    public class SharedPropertyClickDto : ModelDtoBase<SharedPropertyClick>
    {
        public override int Id { get; set; }
        public int SharedPropertyId { get; set; }
        public string MetaData { get; set; }

        public override IModelDto<SharedPropertyClick> From(SharedPropertyClick entity)
        {
            Id = entity.Id;
            MetaData = entity.MetaData;
            SharedPropertyId = entity.SharedPropertyId;
            return this;
        }

        public override SharedPropertyClick Create() =>
            new SharedPropertyClick
            {
                MetaData = "{}",
                SharedPropertyId = SharedPropertyId,
            };

        public override SharedPropertyClick Update() =>
            new SharedPropertyClick
            {
                Id = Id,
                MetaData = MetaData,
                SharedPropertyId = SharedPropertyId,
            };
    }
}
