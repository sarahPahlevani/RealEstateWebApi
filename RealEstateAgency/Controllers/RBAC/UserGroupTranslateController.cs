using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;

namespace RealEstateAgency.Controllers.RBAC
{
    public class UserGroupTranslateController : ModelController<UserGroupTranslate, UserGroupTranslateDto>
    {
        public UserGroupTranslateController(IModelService<UserGroupTranslate, UserGroupTranslateDto> modelService) : base(modelService)
        {
        }

        public override Func<IQueryable<UserGroupTranslate>, IQueryable<UserGroupTranslateDto>> DtoConverter
        => items => items.Select(i => new UserGroupTranslateDto
        {
            Id = i.Id,
            Name = i.Name,
            LanguageId = i.LanguageId,
            UserGroupId = i.UserGroupId
        });
    }
}
