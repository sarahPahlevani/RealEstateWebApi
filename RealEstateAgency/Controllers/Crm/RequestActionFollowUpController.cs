using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;
using RealEstateAgency.Implementations.Authentication;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestActionFollowUpController
        : ModelPagingController<RequestActionFollowUp, RequestActionFollowUpDto, RequestActionFollowUpListDto>
    , IGetFromTenant<RequestActionFollowUpDto>
    {
        private readonly IUserProvider _userProvider;

        public RequestActionFollowUpController(IModelService<RequestActionFollowUp, RequestActionFollowUpDto> modelService, IUserProvider userProvider) : base(modelService)
        {
            _userProvider = userProvider;
            if (!_userProvider.IsAgent) throw new ForbiddenException();
            ModelService.SetBaseFilter(query =>
                query.Where(i => i.RequestActionNavigation.AgentId == _userProvider.AgentId));
        }

        public override Func<IQueryable<RequestActionFollowUp>, IQueryable<RequestActionFollowUpDto>> DtoConverter
        => entities => entities.Select(i => new RequestActionFollowUpDto
        {
            Id = i.Id,
            Description = i.Description,
            ActionTypeId = i.ActionTypeId,
            FollowUpDateTime = i.FollowUpDateTime,
            AgentIdFollowUp = i.AgentIdFollowUp,
            IsDone = i.IsDone,
            RequestActionId = i.RequestActionId,
            FollowUpDateTimeSnooze = i.FollowUpDateTimeSnooze
        }).OrderByDescending(i => i.Id);

        public override Func<IQueryable<RequestActionFollowUp>, IQueryable<RequestActionFollowUpListDto>> PagingConverter
            => entities => entities.Select(i => new RequestActionFollowUpListDto
            {
                Id = i.Id,
                Description = i.Description,
                ActionTypeId = i.ActionTypeId,
                FollowUpDateTime = i.FollowUpDateTime,
                AgentIdFollowUp = i.AgentIdFollowUp,
                IsDone = i.IsDone,
                RequestActionId = i.RequestActionId,
                FollowUpDateTimeSnooze = i.FollowUpDateTimeSnooze,
                ActionType = _userProvider.LanguageId == null ? i.ActionType :
                    i.ActionType.Translate(_userProvider.LanguageId.Value)
            }).OrderBy(i => i.IsDone).ThenByDescending(i => i.Id);

        [HttpGet("GetActionFollowUps/{tenantId}")]
        public async Task<ActionResult<IEnumerable<RequestActionFollowUpDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken) =>
            await DtoConverter(ModelService.AsQueryable(i => i.RequestActionId == tenantId))
                .ToListAsync(cancellationToken);
    }
}
