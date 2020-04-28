using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.CRM;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RealEstateAgency.Shared.Statics;

namespace RealEstateAgency.Controllers.CRM
{
    public class CommissionController : ModelPagingController<Commission, CommissionDto, CommissionListDto>
    {
        private readonly IUserProvider _userProvider;

        public CommissionController(IModelService<Commission, CommissionDto> modelService, IUserProvider userProvider) : base(modelService)
        {
            _userProvider = userProvider;
        }

        public override Func<IQueryable<Commission>, IQueryable<CommissionDto>> DtoConverter => items => items
                .Select(i => new CommissionDto
                {
                    Id = i.Id,
                    CommissionPercent = i.CommissionPercent,
                    Amount = i.Amount,
                    DateCreated = i.DateCreated,
                    IsPay = i.IsPay,
                    PayCode = i.PayCode,
                    PayDate = i.PayDate,
                    Request = i.IdNavigation,
                    UserAccount = i.IdNavigation.UserAccountIdSharedNavigation,
                    Property = i.IdNavigation.PropertyNavigation,
                });


        public override Func<IQueryable<Commission>, IQueryable<CommissionListDto>> PagingConverter => items => items
                .Select(i => new CommissionListDto
                {
                    Id = i.Id,
                    CommissionPercent = i.CommissionPercent,
                    Amount = i.Amount,
                    DateCreated = i.DateCreated,
                    IsPay = i.IsPay,
                    PayCode = i.PayCode,
                    PayDate = i.PayDate,
                    UserAccountId = i.IdNavigation.UserAccountIdSharedNavigation.Id,
                    Username = i.IdNavigation.UserAccountIdSharedNavigation.UserName,
                    PropertyId = i.IdNavigation.PropertyId.GetValueOrDefault(0),
                    PropertyTitle = i.IdNavigation.PropertyNavigation.Title,
                    PropertyPrice = i.IdNavigation.PropertyNavigation.PropertyPrice.Price,
                    PropertyPriceCurrency = i.IdNavigation.PropertyNavigation.PropertyPrice.Currency,
                }).OrderByDescending(i => i.DateCreated);


        [HttpGet("[Action]/{userId}")]
        public async Task<ActionResult<List<CommissionListDto>>> GetByUser(int userId, CancellationToken cancellationToken)
        {
            return await ModelService.Queryable
                .Include(r => r.IdNavigation)
                .Include(r => r.IdNavigation.UserAccountIdSharedNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation.PropertyPrice)
                .Where(r => r.IdNavigation.UserAccountIdShared == userId)
                .Select(i => new CommissionListDto
                {
                    Id = i.Id,
                    CommissionPercent = i.CommissionPercent,
                    Amount = i.Amount,
                    DateCreated = i.DateCreated,
                    IsPay = i.IsPay,
                    PayCode = i.PayCode,
                    PayDate = i.PayDate,
                    UserAccountId = i.IdNavigation.UserAccountIdSharedNavigation.Id,
                    Username = i.IdNavigation.UserAccountIdSharedNavigation.UserName,
                    PropertyId = i.IdNavigation.PropertyId.GetValueOrDefault(0),
                    PropertyTitle = i.IdNavigation.PropertyNavigation.Title,
                    PropertyPrice = i.IdNavigation.PropertyNavigation.PropertyPrice.CalculatedPriceUnit,
                    PropertyPriceCurrency = i.IdNavigation.PropertyNavigation.PropertyPrice.Currency,
                }).OrderByDescending(i => i.DateCreated).ToListAsync(cancellationToken);
        }

        [AllowAnonymous]
        ////[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpGet("[Action]/{currencyId}/{isPay}")]
        public async Task<ActionResult<List<CommissionListDto>>> GetDetails(int currencyId, bool? isPay)
        {
            return await ModelService.Queryable
                .Include(r => r.IdNavigation)
                .Include(r => r.IdNavigation.UserAccountIdSharedNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation.PropertyPrice)
                .Include(r => r.IdNavigation.PropertyNavigation.PropertyPrice.Currency)
                .Where(r => r.IdNavigation.PropertyNavigation.PropertyPrice.CurrencyId == currencyId && (!isPay.HasValue || r.IsPay == isPay.Value))
                .Select(r => new CommissionListDto
                {
                    Id = r.Id,
                    CommissionPercent = r.CommissionPercent,
                    Amount = r.Amount,
                    DateCreated = r.DateCreated,
                    IsPay = r.IsPay,
                    PayCode = r.PayCode,
                    PayDate = r.PayDate,
                    UserAccountId = r.IdNavigation.UserAccountIdSharedNavigation.Id,
                    Username = r.IdNavigation.UserAccountIdSharedNavigation.UserName,
                    PropertyId = r.IdNavigation.PropertyNavigation.Id,
                    PropertyTitle = r.IdNavigation.PropertyNavigation.Title,
                    PropertyPrice = r.IdNavigation.PropertyNavigation.PropertyPrice.CalculatedPriceUnit,
                    PropertyPriceCurrency = r.IdNavigation.PropertyNavigation.PropertyPrice.Currency,
                }).ToListAsync();

            //var db = new RealEstateDbContext();

            //var list = await (from r in db.Commission
            //                  join q in db.Request on r.Id equals q.Id
            //                  join u in db.UserAccount on q.UserAccountIdShared equals u.Id
            //                  join p in db.Property on q.PropertyId equals p.Id
            //                  join pp in db.PropertyPrice on p.Id equals pp.Id
            //                  join pc in db.Currency on pp.CurrencyId equals pc.Id
            //                  where pc.Id == currencyId && (!isPay.HasValue || r.IsPay == isPay.Value)
            //                  select new CommissionListDto
            //                  {
            //                      Id = r.Id,
            //                      CommissionPercent = r.CommissionPercent,
            //                      Amount = r.Amount,
            //                      DateCreated = r.DateCreated,
            //                      IsPay = r.IsPay,
            //                      PayCode = r.PayCode,
            //                      PayDate = r.PayDate,
            //                      UserAccountId = u.Id,
            //                      Username = u.UserName,
            //                      PropertyId = p.Id,
            //                      PropertyTitle = p.Title,
            //                      PropertyPrice = pp.CalculatedPriceUnit,
            //                      PropertyPriceCurrency = pc,
            //                  }).ToListAsync();

            //return list;
        }


        //[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<List<CommissionInfoDto>>> GetCommissionInfo()
        {

            var currencyList = await ModelService.Queryable
                .Include(r => r.IdNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation)
                .Include(r => r.IdNavigation.PropertyNavigation.PropertyPrice)
                .Include(r => r.IdNavigation.PropertyNavigation.PropertyPrice.Currency)
                .Select(r => new CommissionInfoDto
                {
                    CurrencyId = r.IdNavigation.PropertyNavigation.PropertyPrice.CurrencyId,
                    CurrencyName = r.IdNavigation.PropertyNavigation.PropertyPrice.Currency.Name,
                    CurrencySymbol = r.IdNavigation.PropertyNavigation.PropertyPrice.Currency.Symbol,
                    Amount = r.Amount,
                    IsPay = r.IsPay,
                }).ToListAsync();

            return (from r in currencyList
                    group r by r.CurrencyId into gp
                    select new CommissionInfoDto
                    {
                        CurrencyId = gp.Key,
                        CurrencyName = gp.ToList().Select(i => i.CurrencyName).FirstOrDefault(),
                        CurrencySymbol = gp.ToList().Select(i => i.CurrencySymbol).FirstOrDefault(),
                        TotalCommission = gp.ToList().Where(i => !i.IsPay).Sum(i => i.Amount),
                        TotalEarn = gp.ToList().Where(i => i.IsPay).Sum(i => i.Amount),
                    }).ToList();


            //var db = new RealEstateDbContext();

            //var currencyList = await (from r in db.Commission
            //                          join q in db.Request on r.Id equals q.Id
            //                          join p in db.Property on q.PropertyId equals p.Id
            //                          join pp in db.PropertyPrice on p.Id equals pp.Id
            //                          join pc in db.Currency on pp.CurrencyId equals pc.Id
            //                          select new CommissionInfoDto
            //                          {
            //                              CurrencyId = r.IdNavigation.PropertyNavigation.PropertyPrice.CurrencyId,
            //                              CurrencyName = r.IdNavigation.PropertyNavigation.PropertyPrice.Currency.Name,
            //                              CurrencySymbol = r.IdNavigation.PropertyNavigation.PropertyPrice.Currency.Symbol,
            //                              Amount = r.Amount,
            //                              IsPay = r.IsPay,
            //                          }).ToListAsync();


        }



        ////[Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator)]
        //[HttpPost("[Action]")]
        //public async Task<ActionResult<List<CommissionDto_old>>> NewGetUserCommission()
        //{
        //    List<CommissionDto_old> list = new List<CommissionDto_old>();
        //    list = await (from r in ModelService.DbContext.Request
        //                  join p in ModelService.DbContext.Property on r.PropertyId equals p.Id
        //                  join u in ModelService.DbContext.UserAccount on r.UserAccountIdShared equals u.Id
        //                  join pp in ModelService.DbContext.PropertyPrice on r.PropertyId equals pp.Id
        //                  where r.UserAccountIdShared != null && r.IsDone == true && r.IsSuccess == true
        //                  //&& r.UserAccountIdShared == _userProvider.Id
        //                  select new CommissionDto_old
        //                  {
        //                      CommissionPercent = r.Commission == null ? 0 : r.Commission,
        //                      PropertyId = p.Id,
        //                      PropertyPrice = pp.Price,
        //                      PropertyTitle = p.Title,
        //                      TotalCommission = pp.Price - (pp.Price - ((pp.Price * (r.Commission == null ? 0 : r.Commission)) / 100)),
        //                      DateCreated = r.DateCreated,
        //                      RequesterFullname = r.RequesterFullname,
        //                      UserName = u.UserName,
        //                      CurrencySymbol = pp.Currency.Symbol
        //                  }).ToListAsync();

        //    return list;
        //}


    }
}
