﻿using System;
using System.Linq;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.SharingSystem;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateAgency.Controllers.SharingSystem
{
    [AllowAnonymous]
    public class SocialNetworkController : ModelController<SocialNetwork, SocialNetworkDto>
    {
        public SocialNetworkController(IModelService<SocialNetwork, SocialNetworkDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<SocialNetwork>, IQueryable<SocialNetworkDto>> DtoConverter =>
            items => items.Select(i => new SocialNetworkDto
            {
                Id = i.Id,
                Name = i.Name,
                UniqueKey = i.UniqueKey,
                Icon = i.Icon,
                Url = i.Url,
            });
    }
}
