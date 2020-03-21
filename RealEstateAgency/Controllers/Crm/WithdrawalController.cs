using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Infrastructure;
using System;
using System.Linq;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Dtos.ModelDtos.Crm;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using System.Collections.Generic;

namespace RealEstateAgency.Controllers.Infrastructure
{
    public class WithdrawalController : ModelPagingController<Withdrawal, WithdrawalDto, WithdrawalListDto>
    {
        private readonly IUserProvider _userProvider;

        public WithdrawalController(IModelService<Withdrawal, WithdrawalDto> modelService, IUserProvider userProvider) : base(modelService)
        {
            _userProvider = userProvider;
        }

        public override Func<IQueryable<Withdrawal>, IQueryable<WithdrawalDto>> DtoConverter => _converter;
        public override Func<IQueryable<Withdrawal>, IQueryable<WithdrawalListDto>> PagingConverter => items => items
                .Select(i => new WithdrawalListDto
                {
                    Id = i.Id,
                    UserAccountId = i.UserAccountId,
                    UserAccount = i.UserAccount,
                    Amount = i.Amount,
                    DateCreated = i.DateCreated,
                }).OrderByDescending(i => i.DateCreated);

        private Func<IQueryable<Withdrawal>, IQueryable<WithdrawalDto>> _converter =>
            entities => entities.Select(i =>
                new WithdrawalDto
                {
                    Id = i.Id,
                    UserAccountId = i.UserAccountId,
                    UserAccount = i.UserAccount,
                    Amount = i.Amount,
                    DateCreated = i.DateCreated,
                });

        [HttpGet("[Action]")]
        public ActionResult<List<WithdrawalListDto>> GetByUser()
        {
            return ModelService.Queryable.Where(r => r.UserAccountId == _userProvider.Id)
                .Select(r => new WithdrawalListDto
                {
                    Id = r.Id,
                    UserAccountId = r.UserAccountId,
                    UserAccount = r.UserAccount,
                    Amount = r.Amount,
                    DateCreated = r.DateCreated,
                }).OrderByDescending(r => r.DateCreated).ToList();
        }
    }
}
