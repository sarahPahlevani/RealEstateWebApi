using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.ApiImplementations.Services.Exceptions;

namespace RealEstateAgency.Controllers.Crm
{
    public class RequestActionController : ModelPagingController<RequestAction, RequestActionDto, RequestActionListDto>
        , IGetFromTenant<RequestActionListDto>
    {
        private readonly IUserProvider _userProvider;
        private readonly IEntityService<RequestActionFollowUp> _followUpService;

        public RequestActionController(IModelService<RequestAction, RequestActionDto> modelService, IUserProvider userProvider
        , IEntityService<RequestActionFollowUp> followUpService) : base(modelService)
        {
            if (!userProvider.IsAgent) throw new ForbiddenException();
            _userProvider = userProvider;
            _followUpService = followUpService;
            modelService.SetBaseFilter(query =>
                query.Where(i => i.AgentId == userProvider.AgentId));
        }

        public override async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var followUps = (await _followUpService.GetAllAsync(i => i.RequestActionId == id, cancellationToken)).ToList();
                    var followUpIds = followUps.Select(f => f.Id).ToList();
                    var relatedActions = await ModelService.GetAllAsync(a => a.RequestActionFollowUpReference != null
                                                                       && followUpIds.Contains(
                                                                           a.RequestActionFollowUpReference.Value),
                        cancellationToken);
                    foreach (var relatedAction in relatedActions)
                    {
                        relatedAction.RequestActionFollowUpReference = null;
                        await ModelService.UpdateAsync(relatedAction, cancellationToken);
                    }
                    await _followUpService.DeleteRangeAsync(followUps,
                        cancellationToken);
                    var res = await base.Delete(id, cancellationToken);
                    scope.Complete();
                    return res;
                }
                catch (Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }

        public override Func<IQueryable<RequestAction>, IQueryable<RequestActionDto>> DtoConverter
        => items => items.Select(i => new RequestActionDto
        {
            Id = i.Id,
            RequestId = i.RequestId,
            ActionTypeId = i.ActionTypeId,
            AgentId = i.AgentId,
            RequestActionFollowUpReference = i.RequestActionFollowUpReference,
            Description = i.Description,
            ActionDate = i.ActionDate,
            ActionTime = i.ActionTime,
            ActionIsSuccess = i.ActionIsSuccess,
            MetaDataJson = i.MetaDataJson,
        });

        public override Func<IQueryable<RequestAction>, IQueryable<RequestActionListDto>> PagingConverter
            => items => items.Include(i => i.Request.UserAccountIdRequesterNavigation)
                .Include(i => i.RequestActionFollowUp)
                .Select(i => new RequestActionListDto
                {
                    Id = i.Id,
                    Description = i.Description,
                    RequestId = i.RequestId,
                    Request = i.Request,
                    ActionTypeId = i.ActionTypeId,
                    AgentId = i.AgentId,
                    ActionIsSuccess = i.ActionIsSuccess,
                    ActionDate = i.ActionDate,
                    RequestActionFollowUpReference = i.RequestActionFollowUpReference,
                    ActionFollowUps = i.RequestActionFollowUp.ToList(),
                    ActionTime = i.ActionTime,
                    ActionType = _userProvider.LanguageId == null ? i.ActionType :
                        i.ActionType.Translate(_userProvider.LanguageId.Value)
                }).OrderBy(i => i.ActionIsSuccess).ThenByDescending(i => i.Id);


        [HttpGet("GetRequestActions/{tenantId}")]
        public async Task<ActionResult<IEnumerable<RequestActionListDto>>> GetFromTenant(int tenantId, CancellationToken cancellationToken) =>
            await PagingConverter(ModelService.AsQueryable(i => i.RequestId == tenantId))
                .ToListAsync(cancellationToken);

    }
}
