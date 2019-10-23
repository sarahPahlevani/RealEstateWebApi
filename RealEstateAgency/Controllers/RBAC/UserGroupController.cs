﻿using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.RBAC
{
    public class UserGroupController : ModelController<UserGroup, UserGroupDto>
    {
        public UserGroupController(IModelService<UserGroup, UserGroupDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<UserGroup>, IQueryable<UserGroupDto>> DtoConverter =>
            items => items.Select(i => new UserGroupDto
            {
                Id = i.Id,
                Name = i.Name,
                StaticCode = i.StaticCode
            });
    }
}
