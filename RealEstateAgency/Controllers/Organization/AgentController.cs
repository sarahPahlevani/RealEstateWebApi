using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Shared.Statics;
using RealEstateAgency.Dtos.ModelDtos.RBAC;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Services;//

namespace RealEstateAgency.Controllers.Organization
{
    public class WebAppAgentDto : UserAccountDto
    {
        public string Description { get; set; }
    }

    public class AgentController : ModelPagingController<Agent, AgentDto, AgentListDto>
    {
        private readonly IEntityService<UserAccount> _userAccountService;
        private readonly IFastHasher _fastHasher;
        private readonly IUserProvider _userProvider;
        private readonly IPasswordService _passwordService;
        private readonly IPathProvider _pathProvider;

        public AgentController(IModelService<Agent, AgentDto> modelService,
            IEntityService<UserAccount> userAccountService,
            IFastHasher fastHasher,
            IUserProvider userProvider,
            IPasswordService passwordService,
            IPathProvider pathProvider) : base(modelService)
        {
            _userAccountService = userAccountService;
            _fastHasher = fastHasher;
            _userProvider = userProvider;
            _passwordService = passwordService;
            _pathProvider = pathProvider;
            modelService.SetBaseFilter(filter => filter.Where(a => a.RealEstateId == _userProvider.RealEstateId && !a.Deleted && a.UserAccount.IsActive != false));
        }

        public override Func<IQueryable<Agent>, IQueryable<AgentDto>> DtoConverter
            => items => items.Select(i => new AgentDto
            {
                Id = i.Id,
                Description = i.Description,
                MetadataJson = i.MetadataJson,
                RealEstateId = i.RealEstateId,
                UserAccountId = i.UserAccountId
            });

        public override Func<IQueryable<Agent>, IQueryable<AgentListDto>> PagingConverter =>
        items => items.Include(i => i.UserAccount).Select(i => new AgentListDto
        {
            Id = i.Id,
            Description = i.Description,
            UserAccountId = i.UserAccountId,
            HasPublishingAuthorization = i.HasPublishingAuthorization,
            IsResponsible = i.IsResponsible,
            RealEstateId = i.RealEstateId,
            UserAccount = i.UserAccount
        });

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.Agent + "," + UserGroups.RealEstateAdministrator)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<IEnumerable<UserAccountDto>>> GetAgents(CancellationToken cancellationToken)
        {
            var agents = await ModelService.AsQueryable(/*a => a.RealEstateId == _userProvider.RealEstateId*/)
                .Include(i => i.UserAccount)
                .Select(i => new UserAccountDto
                {
                    AgentId = i.Id,
                    Id = i.UserAccountId,
                    AuthenticationProviderId = i.UserAccount.AuthenticationProviderId,
                    AuthenticationProviderAccessToken = i.UserAccount.AuthenticationProviderAccessToken,
                    UserName = i.UserAccount.UserName,
                    IsActive = i.UserAccount.IsActive,
                    IsConfirmed = i.UserAccount.IsConfirmed,
                    HasExternalAuthentication = i.UserAccount.HasExternalAuthentication,
                    ActivationKey = i.UserAccount.ActivationKey,
                    ResetPasswordKey = i.UserAccount.ResetPasswordKey,
                    RegistrationDate = i.UserAccount.RegistrationDate,
                    FirstName = i.UserAccount.FirstName,
                    LastName = i.UserAccount.LastName,
                    MiddleName = i.UserAccount.MiddleName,
                    Email = i.UserAccount.Email,
                    Phone01 = i.UserAccount.Phone01,
                    Phone02 = i.UserAccount.Phone01,
                    Address01 = i.UserAccount.Address01,
                    Address02 = i.UserAccount.Address01,
                    ZipCode = i.UserAccount.ZipCode,
                }).ToListAsync(cancellationToken);
            agents.ForEach(a => a.PasswordHash = null);

            return agents;
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPost("[Action]")]
        public async Task<ActionResult<AgentAccountDto>> CreateAgent([FromBody]AgentAccountDto dto, CancellationToken cancellationToken)
        {
            if (_userProvider.RealEstateId is null) throw new AppException("you cannot create new agent please try again later");
            var dupUser = await _userAccountService.GetAsync(u => u.Email == dto.Email || u.UserName == dto.UserName,
                cancellationToken);
            if (dupUser != null)
            {
                if (dupUser.Email == dto.Email) throw new AppException("There is another user registered with this email address. Please chose another one.");
                if (dupUser.UserName == dto.UserName) throw new AppException("There is another user registered with this username. Please chose another one.");
            }

            var passwordHash = _passwordService.HashUserPassword(dto.Email, dto.Password);
            var user = await _userAccountService.CreateAsync(new UserAccount
            {
                ActivationKey = _fastHasher.CalculateHash(dto.FirstName + dto.LastName + dto.Email + dto.UserName),
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                HasExternalAuthentication = false,
                IsConfirmed = false,
                PasswordHash = passwordHash,
                RegistrationDate = DateTime.Now,
                Address01 = dto.Address01,
                Address02 = dto.Address02,
                Phone01 = dto.Phone01,
                Phone02 = dto.Phone02,
                IsActive = true,
                MiddleName = dto.MiddleName,
                ReferralCode = _fastHasher.CalculateTimeHash(dto.Email),
                ZipCode = dto.ZipCode,
            }, cancellationToken);

            dto.UserId = user.Id;

            var agent = await ModelService.CreateAsync(new Agent
            {
                Description = dto.Description,
                HasPublishingAuthorization = dto.HasPublishingAuthorization,
                IsResponsible = dto.IsResponsible,
                RealEstateId = _userProvider.RealEstateId.Value,
                MetadataJson = "{}",
                UserAccountId = user.Id,
            }, cancellationToken);

            dto.AgentId = agent.Id;
            return CreatedAtAction(nameof(CreateAgent), dto);
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override async Task<ActionResult<PageResultDto<AgentListDto>>> GetPageAsync(
            [FromBody] PageRequestFilterDto filterDto, CancellationToken cancellationToken) =>
            await GetPageResultAsync(ModelService.Queryable,
                filterDto, filterDto.Filter.ToObject<AgentListFilter>(),
                cancellationToken);

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override Task<ActionResult<PageResultDto<AgentListDto>>> GetPageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
            => base.GetPageAsync(pageSize, pageNumber, cancellationToken);

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> UpdateAgent([FromBody]AgentAccountDto dto, CancellationToken cancellationToken)
        {
            var user = await _userAccountService.GetAsync(u => u.Id == dto.UserId, cancellationToken);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            if (dto.UpdatePassword)
                user.PasswordHash = _passwordService.HashUserPassword(dto.Email, dto.Password);
            user.Address01 = dto.Address01;
            user.Address02 = dto.Address02;
            user.Phone01 = dto.Phone01;
            user.Phone02 = dto.Phone02;
            user.MiddleName = dto.MiddleName;
            user.ZipCode = dto.ZipCode;

            await _userAccountService.UpdateAsync(user, cancellationToken);

            var agent = await ModelService.GetAsync(a => a.Id == dto.AgentId, cancellationToken);
            agent.Description = dto.Description;
            agent.HasPublishingAuthorization = dto.HasPublishingAuthorization;
            agent.IsResponsible = dto.IsResponsible;

            await ModelService.UpdateAsync(agent, cancellationToken);

            return NoContent();
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpGet("[Action]/{userId}")]
        public async Task<ActionResult<AgentAccountDto>> GetAgentByUserId(int userId, CancellationToken cancellationToken)
        {
            var item = await new RealEstateDbContext().Agent.Where(r => r.UserAccountId == userId)
                .Select(i => new AgentAccountDto
                {
                    Description = i.Description,
                    Email = i.UserAccount.Email,
                    AgentId = i.Id,
                    UserName = i.UserAccount.UserName,
                    HasPublishingAuthorization = i.HasPublishingAuthorization,
                    Phone01 = i.UserAccount.Phone01,
                    IsResponsible = i.IsResponsible,
                    Address01 = i.UserAccount.Address01,
                    LastName = i.UserAccount.LastName,
                    ZipCode = i.UserAccount.ZipCode,
                    FirstName = i.UserAccount.FirstName,
                    Phone02 = i.UserAccount.Phone02,
                    Address02 = i.UserAccount.Address02,
                    MiddleName = i.UserAccount.MiddleName,
                    UserId = i.UserAccountId
                }).FirstOrDefaultAsync(cancellationToken);

            //var user = await ModelService.AsQueryable(i => i.UserAccountId == userId)
            //    .Select(i => new AgentAccountDto
            //    {
            //        Description = i.Description,
            //        Email = i.UserAccount.Email,
            //        AgentId = i.Id,
            //        UserName = i.UserAccount.UserName,
            //        HasPublishingAuthorization = i.HasPublishingAuthorization,
            //        Phone01 = i.UserAccount.Phone01,
            //        IsResponsible = i.IsResponsible,
            //        Address01 = i.UserAccount.Address01,
            //        LastName = i.UserAccount.LastName,
            //        ZipCode = i.UserAccount.ZipCode,
            //        FirstName = i.UserAccount.FirstName,
            //        Phone02 = i.UserAccount.Phone02,
            //        Address02 = i.UserAccount.Address02,
            //        MiddleName = i.UserAccount.MiddleName,
            //        UserId = i.UserAccountId
            //    }).FirstOrDefaultAsync(cancellationToken);

            if (item is null) return NotFound();
            return item;
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpGet("[Action]/{agentId}")]
        public async Task<ActionResult<AgentAccountDto>> GetAgentForUpdate(int agentId, CancellationToken cancellationToken)
        {
            var user = await ModelService.AsQueryable(i => i.Id == agentId)
                .Select(i => new AgentAccountDto
                {
                    Description = i.Description,
                    Email = i.UserAccount.Email,
                    AgentId = i.Id,
                    UserName = i.UserAccount.UserName,
                    HasPublishingAuthorization = i.HasPublishingAuthorization,
                    Phone01 = i.UserAccount.Phone01,
                    IsResponsible = i.IsResponsible,
                    Address01 = i.UserAccount.Address01,
                    LastName = i.UserAccount.LastName,
                    ZipCode = i.UserAccount.ZipCode,
                    FirstName = i.UserAccount.FirstName,
                    Phone02 = i.UserAccount.Phone02,
                    Address02 = i.UserAccount.Address02,
                    MiddleName = i.UserAccount.MiddleName,
                    UserId = i.UserAccountId
                }).FirstOrDefaultAsync(cancellationToken);
            if (user is null) return NotFound();
            return user;
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            //var user = await _userAccountService.GetAsync(u => u.AgentUserAccount.Any(a => a.Id == id), cancellationToken);
            //user.Email = _fastHasher.CalculateTimeHash(user.Email);
            //user.UserName = _fastHasher.CalculateTimeHash(user.UserName);
            //user.IsActive = false;
            //await _userAccountService.UpdateAsync(user, cancellationToken);
            return await base.Delete(id, cancellationToken);
        }

        [HttpGet("[Action]")]
        [AllowAnonymous]
        public async Task<IEnumerable<WebAppAgentDto>> GetWebAppAgents(CancellationToken cancellationToken)
        {
            var agents = await ModelService.DbContext.Agent
                .Include(i => i.UserAccount)
                .Where(i => !i.Deleted)
                .Select(i => new WebAppAgentDto
                {
                    Id = i.UserAccount.Id,
                    UserName = i.UserAccount.UserName,
                    Address01 = i.UserAccount.Address01,
                    Address02 = i.UserAccount.Address02,
                    AgentId = i.Id,
                    Description = i.Description,
                    ZipCode = i.UserAccount.ZipCode,
                    Email = i.UserAccount.Email,
                    Phone01 = i.UserAccount.Phone01,
                    Phone02 = i.UserAccount.Phone02,
                    FirstName = i.UserAccount.FirstName,
                    MiddleName = i.UserAccount.MiddleName,
                    IsResponsible = i.IsResponsible,
                    HasPublishingAuthorization = i.UserAccount.HasExternalAuthentication,
                    RegistrationDate = i.UserAccount.RegistrationDate,
                    LastName = i.UserAccount.LastName,
                }).ToListAsync(cancellationToken);
            agents.ForEach(a =>
            {
                a.UserPicture = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPicture), a.Id.ToString());
                a.UserPictureTumblr = _pathProvider.GetImageApiPath<UserAccount>(nameof(UserAccount.UserPictureTumblr), a.Id.ToString());
            });
            return agents;
        }
    }
}
