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
using RealEstateAgency.Shared.Services;

namespace RealEstateAgency.Controllers.RBAC
{
    public class WebAppUserProfileDto : UserAccountDto
    {

    }

    public class UserAccountController : ModelPagingController<UserAccount, UserAccountDto, UserAccountListDto>
    {
        private readonly IUserProvider _userProvider;
        private readonly IUserGroupProvider _groupProvider;
        private readonly IPathProvider _pathProvider;
        private readonly IFastHasher _fastHasher;

        public UserAccountController(IModelService<UserAccount, UserAccountDto> modelService,
            IUserProvider userProvider, IUserGroupProvider groupProvider, IFastHasher fastHasher, IPathProvider pathProvider) : base(modelService)
        {
            _userProvider = userProvider;
            _pathProvider = pathProvider;
            _fastHasher = fastHasher;
            _groupProvider = groupProvider;
            var administratorGroupId = groupProvider[UserGroup.Administrator].Id;
            modelService.SetBaseFilter(i => i.Where(u => u.IsActive == true && u.UserAccountGroup.FirstOrDefault(g => g.UserAccountId == u.Id).UserGroupId
                                                         != administratorGroupId));
        }

        public override Func<IQueryable<UserAccount>, IQueryable<UserAccountDto>> DtoConverter =>
            items => items.Select(i => new UserAccountDto
            {
                Id = i.Id,
                UserName = i.UserName,
                FirstName = i.FirstName,
                LastName = i.LastName,
                MiddleName = i.MiddleName,
                IsActive = i.IsActive,
                IsConfirmed = i.IsConfirmed,
                Phone01 = i.Phone01,
                Phone02 = i.Phone02,
                CountryId = i.CountryId,
                CountryName = i.Country == null ? "" : i.Country.Name,
                City = i.City,
                Address01 = i.Address01,
                Address02 = i.Address02,
                Email = i.Email,
                ActivationKey = i.ActivationKey,
                AuthenticationProviderAccessToken = i.AuthenticationProviderAccessToken,
                AuthenticationProviderId = i.AuthenticationProviderId,
                HasExternalAuthentication = i.HasExternalAuthentication,
                ZipCode = i.ZipCode,
                VatCode = i.VatCode,
                RegistrationDate = i.RegistrationDate,
            });

        public override Func<IQueryable<UserAccount>, IQueryable<UserAccountListDto>> PagingConverter =>
            items => items.Select(i => new UserAccountListDto
            {
                Id = i.Id,
                UserName = i.UserName,
                FirstName = i.FirstName,
                LastName = i.LastName,
                MiddleName = i.MiddleName,
                IsActive = i.IsActive,
                IsConfirmed = i.IsConfirmed,
                ZipCode = i.ZipCode,
                VatCode = i.VatCode,
                CountryId = i.CountryId,
                CountryName = i.Country == null ? "" : i.Country.Name,
                City = i.City,
                Address01 = i.Address01,
                Address02 = i.Address02,
                Email = i.Email,
                ActivationKey = i.ActivationKey,
                HasExternalAuthentication = i.HasExternalAuthentication,
                Phone01 = i.Phone01,
                Phone02 = i.Phone02,
                RegistrationDate = i.RegistrationDate,
                UserGroupName = i.UserAccountGroup.FirstOrDefault(g => g.UserAccountId == i.Id).UserGroup.Name,
                //TotalEarn = (from te in i.RequestUserAccountIdSharedNavigation
                //             where te.IsDone && te.IsSuccess
                //             select te.PropertyNavigation.PropertyPrice.Price - (te.PropertyNavigation.PropertyPrice.Price * (te.Commission.GetValueOrDefault(0)) / 100)).FirstOrDefault(),
                //TotalWithdrawal = (from te in i.RequestUserAccountIdSharedNavigation
                //                   where te.IsDone && te.IsSuccess
                //                   select te.PropertyNavigation.PropertyPrice.Price - (te.PropertyNavigation.PropertyPrice.Price * (te.Commission.GetValueOrDefault(0)) / 100)).FirstOrDefault(),
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
                    CountryId = u.CountryId,
                    CountryName = u.Country == null ? "" : u.Country.Name,
                    City = u.City,
                    Address01 = u.Address01,
                    Address02 = u.Address02,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsActive = u.IsActive,
                    IsConfirmed = u.IsConfirmed,
                    MiddleName = u.MiddleName,
                    ZipCode = u.ZipCode,
                    VatCode = u.VatCode,
                    Phone01 = u.Phone01,
                    Phone02 = u.Phone02,
                    RegistrationDate = u.RegistrationDate,
                }).FirstAsync(cancellationToken);

            user.UserPicture = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPicture), user.Id.ToString());
            user.UserPictureTumblr = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPictureTumblr), user.Id.ToString());
            return user;
        }


        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await ModelService.GetAsync(u => u.Id == id, cancellationToken);
            if(user is null)
                return NoContent();

            user.Email = _fastHasher.CalculateTimeHash(user.Email);
            user.UserName = _fastHasher.CalculateTimeHash(user.UserName);
            user.IsActive = false;
            await ModelService.UpdateAsync(user, cancellationToken);
            
            return NoContent();

            //return await base.Delete(id, cancellationToken);
        }

        //[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override async Task<ActionResult> UpdateAsync(UserAccountDto value, CancellationToken cancellationToken)
        {
            if (_userProvider.Id != value.Id && _userProvider.GroupId != _groupProvider[UserGroup.Administrator].Id && _userProvider.GroupId != _groupProvider[UserGroup.RealEstateAdministrator].Id)
                return Forbid();

            var user = await ModelService.GetAsync(u => u.Id == value.Id, cancellationToken);
            if (user is null)
                return NoContent();

            //user.Email = _fastHasher.CalculateTimeHash(user.Email);
            //user.UserName = _fastHasher.CalculateTimeHash(user.UserName);
            //user.IsActive = false;

            user.FirstName = value.FirstName;
            user.LastName = value.LastName;
            user.MiddleName = value.MiddleName;
            user.UserName = value.UserName;
            user.Email = value.Email;
            user.Phone01 = value.Phone01;
            user.Phone02 = value.Phone02;
            user.CountryId = value.CountryId;
            user.City = value.City;
            user.Address01 = value.Address01;
            user.Address02 = value.Address02;
            user.ZipCode = value.ZipCode;
            user.VatCode = value.VatCode;

            await ModelService.UpdateAsync(user, cancellationToken);

            return NoContent();

            //return await base.Delete(id, cancellationToken);
        }


    }
}
