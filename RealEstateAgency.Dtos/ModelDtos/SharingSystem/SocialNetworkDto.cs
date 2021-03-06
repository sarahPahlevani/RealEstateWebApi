﻿using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.SharingSystem
{
    public class SocialNetworkDto : ModelDtoBase<SocialNetwork>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string UniqueKey { get; set; }
        public string Url { get; set; }

        public override IModelDto<SocialNetwork> From(SocialNetwork entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Icon = entity.Icon;
            UniqueKey = entity.UniqueKey;
            Url = entity.Url;
            return this;
        }

        public override SocialNetwork Create() =>
            new SocialNetwork
            {
                Name = Name,
                Icon = Icon,
                UniqueKey = UniqueKey,
                Url = Url,
            };

        public override SocialNetwork Update() =>
            new SocialNetwork
            {
                Id = Id,
                Name = Name,
                Icon = Icon,
                UniqueKey = UniqueKey,
                Url = Url,
            };
    }
}
