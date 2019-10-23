using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Authentication.Contracts;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Statics;
using UserGroup = RealEstateAgency.Dtos.Other.UserGroup.UserGroup;

namespace RealEstateAgency.Controllers.RBAC
{
    public class WebAppUserProfileDto : UserAccountDto
    {

    }

    public class UserAccountController : ModelPagingController<UserAccount, UserAccountDto, UserAccountListDto>
    {
        private readonly IUserProvider _userProvider;
        private readonly IPathProvider _pathProvider;

        public UserAccountController(IModelService<UserAccount, UserAccountDto> modelService,
            IUserProvider userProvider, IUserGroupProvider groupProvider
            , IPathProvider pathProvider) : base(modelService)
        {
            _userProvider = userProvider;
            _pathProvider = pathProvider;
            var administratorGroupId = groupProvider[UserGroup.Administrator].Id;
            modelService.SetBaseFilter(i => i.Where(u => u.UserAccountGroup.FirstOrDefault(g => g.UserAccountId == u.Id).UserGroupId
                                                         != administratorGroupId));
        }

        public override Func<IQueryable<UserAccount>, IQueryable<UserAccountDto>> DtoConverter =>
            items => items.Select(i => new UserAccountDto
            {
                Id = i.Id,
                Phone01 = i.Phone01,
                IsActive = i.IsActive,
                ZipCode = i.ZipCode,
                Address01 = i.Address01,
                Address02 = i.Address01,
                Email = i.Email,
                ActivationKey = i.ActivationKey,
                AuthenticationProviderAccessToken = i.AuthenticationProviderAccessToken,
                AuthenticationProviderId = i.AuthenticationProviderId,
                FirstName = i.FirstName,
                HasExternalAuthentication = i.HasExternalAuthentication,
                IsConfirmed = i.IsConfirmed,
                LastName = i.LastName,
                MiddleName = i.MiddleName,
                Phone02 = i.Phone02,
                RegistrationDate = i.RegistrationDate,
                UserName = i.UserName
            });

        public override Func<IQueryable<UserAccount>, IQueryable<UserAccountListDto>> PagingConverter =>
            items => items.Select(i => new UserAccountListDto
            {
                Id = i.Id,
                Phone01 = i.Phone01,
                IsActive = i.IsActive,
                ZipCode = i.ZipCode,
                Address01 = i.Address01,
                Address02 = i.Address01,
                Email = i.Email,
                ActivationKey = i.ActivationKey,
                FirstName = i.FirstName,
                HasExternalAuthentication = i.HasExternalAuthentication,
                IsConfirmed = i.IsConfirmed,
                LastName = i.LastName,
                MiddleName = i.MiddleName,
                Phone02 = i.Phone02,
                RegistrationDate = i.RegistrationDate,
                UserName = i.UserName,
                UserGroupName = i.UserAccountGroup
                    .FirstOrDefault(g => g.UserAccountId == i.Id).UserGroup.Name
            });

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override async Task<ActionResult<PageResultDto<UserAccountListDto>>> GetPageAsync(
            [FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken) =>
            await GetPageResultAsync(ModelService.Queryable,
                requestDto, requestDto.Filter.ToObject<UserAccountListFilter>(),
                cancellationToken);

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override Task<ActionResult<PageResultDto<UserAccountListDto>>>
            GetPageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
            => base.GetPageAsync(pageSize, pageNumber, cancellationToken);

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> SetUserActivation([FromBody] SetUserActivationDto dto, CancellationToken token)
        {
            if (_userProvider.Id == dto.UserId) throw new AppException("You cannot change your activation state");
            var user = await ModelService.GetAsync(i => i.Id == dto.UserId, token);

            user.IsActive = dto.ActivationState;

            await ModelService.UpdateAsync(user, token);

            return NoContent();
        }

        [HttpGet("[Action]")]
        public async Task<WebAppUserProfileDto> Profile(CancellationToken cancellationToken)
        {
            var user = await ModelService.AsQueryable(u => u.Id == _userProvider.Id)
                .Select(u => new WebAppUserProfileDto
                {
                    Id = u.Id,
                    LanguageId = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    Address01 = u.Address01,
                    Address02 = u.Address02,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsActive = u.IsActive,
                    IsConfirmed = u.IsConfirmed,
                    MiddleName = u.MiddleName,
                    ZipCode = u.ZipCode,
                    Phone01 = u.Phone01,
                    Phone02 = u.Phone02,
                    RegistrationDate = u.RegistrationDate,
                })
                .FirstAsync(cancellationToken);

            user.UserPicture = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPicture), user.Id.ToString());
            user.UserPictureTumblr = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPictureTumblr), user.Id.ToString());
            return user;
        }
    }
}
