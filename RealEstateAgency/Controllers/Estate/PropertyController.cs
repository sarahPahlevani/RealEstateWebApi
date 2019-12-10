using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Implementations.ApiImplementations.Services.Contracts;
using RealEstateAgency.Controllers.Contracts;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using RealEstateAgency.Dtos.Other;
using RealEstateAgency.Dtos.Other.PaginationListDtos;
using RealEstateAgency.Implementations.Providers;
using RealEstateAgency.Shared.Exceptions;
using RealEstateAgency.Shared.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using RealEstateAgency.Implementations.Authentication;
using RealEstateAgency.Implementations.ApiImplementations.Models;

namespace RealEstateAgency.Controllers.Estate
{
    public partial class PropertyController : ModelPagingController<Property, PropertyDto, PropertyPaginationListDto>
    {
        private readonly IUserProvider _userProvider;
        private readonly IPathProvider _pathProvider;

        public PropertyController(IModelService<Property, PropertyDto> modelService,
            IUserProvider userProvider, IPathProvider pathProvider) : base(modelService)
        {
            _userProvider = userProvider;
            _pathProvider = pathProvider;
        }

        public override async Task<ActionResult<PageResultDto<PropertyPaginationListDto>>> GetPageAsync([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken) =>
            await GetPageResultAsync(ModelService.Queryable, requestDto, requestDto.Filter.ToObject<PropertyListFilter>(),
                cancellationToken);

        public override Func<IQueryable<Property>, IQueryable<PropertyPaginationListDto>> PagingConverter =>
            items => items.Include(i => i.UserAccountIdReadyForPublishNavigation)
                .Include(i => i.UserAccountIdPublishedNavigation).Select(p => new PropertyPaginationListDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    VideoUrl = p.VideoUrl,
                    CompletedSections = 1,
                    Price = p.PropertyPrice != null ? p.PropertyPrice.Price : 0,
                    CalculatedPriceUnit = p.PropertyPrice != null ? p.PropertyPrice.CalculatedPriceUnit : 0,
                    PropertyLabelName = p.PropertyLabel.Name,
                    PropertyStatusName = p.PropertyStatus.Name,
                    PropertyTypeName = p.PropertyType.Name,
                    ZipCode = p.PropertyLocation == null ? "Not set" : p.PropertyLocation.ZipCode,
                    ReadyForPublish = p.ReadyForPublish,
                    PublishingDate = p.PublishingDate,
                    IsPublished = p.IsPublished,
                    UserAccountPublished = p.UserAccountIdPublishedNavigation,
                    ReadyForPublishDate = p.ReadyForPublishDate,
                    UserAccountReadyForPublish = p.UserAccountIdReadyForPublishNavigation,
                    CheckReadyPublish = !string.IsNullOrEmpty(p.Title) && p.PropertyDetail != null && p.PropertyPrice != null && p.PropertyImage.Any()
                }).OrderByDescending(i => i.Id);

        public override Func<IQueryable<Property>, IQueryable<PropertyDto>> DtoConverter =>
            items => items.Select(i => new PropertyDto
            {
                Id = i.Id,
                PropertyTypeId = i.PropertyTypeId,
                PropertyLabelId = i.PropertyLabelId,
                PropertyStatusId = i.PropertyStatusId,
                Title = i.Title,
                PropertyUniqId = i.PropertyUniqId,
                Description = i.Description,
                VideoUrl = i.VideoUrl,
            });

        [HttpGet("[Action]/{propertyId}")]
        public async Task<ActionResult<PropertySummeryDto>> GetPropertySummery(int propertyId, CancellationToken cancellationToken)
        {
            var propertySummery = await ModelService.Queryable
                .Include(p => p.PropertyType)
                .Include("PropertyType.PropertyTypeTranslate")
                .Include(p => p.PropertyLocation)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyPrice)
                .Include(p => p.PropertyImage)
                .Select(p => new PropertySummeryDto
                {
                    Id = p.Id,
                    DateUpdated = p.DateUpdated,
                    Title = p.Title,
                    PropertyTypeId = p.PropertyTypeId,
                    PropertyLabelId = p.PropertyLabelId,
                    PropertyStatusId = p.PropertyStatusId,
                    CityId = p.PropertyLocation == null ? 0 : p.PropertyLocation.CityId,
                    RegionId = p.PropertyLocation == null ? 0 : p.PropertyLocation.RegionId,
                    Price = p.PropertyPrice == null ? 0 : p.PropertyPrice.Price,
                    CalculatedPriceUnit = p.PropertyPrice == null ? 0 : p.PropertyPrice.CalculatedPriceUnit,
                    CurrencyId = p.PropertyPrice == null ? 0 : p.PropertyPrice.CurrencyId,
                    PriceScaleUnitId = p.PropertyPrice == null ? 0 : p.PropertyPrice.PriceScaleUnitId,
                    Size = p.PropertyDetail == null ? 0 : p.PropertyDetail.Size,
                    Bedrooms = p.PropertyDetail == null ? 0 : p.PropertyDetail.Bedrooms,
                    Bathrooms = p.PropertyDetail == null ? 0 : p.PropertyDetail.Bathrooms,
                    Garages = p.PropertyDetail == null ? 0 : p.PropertyDetail.Garages,
                    Rooms = p.PropertyDetail == null ? 0 : p.PropertyDetail.Rooms,
                    YearBuild = p.PropertyDetail == null ? 0 : p.PropertyDetail.YearBuild,
                    Images = p.PropertyImage.Where(i => !i.Deleted),
                }).FirstOrDefaultAsync(p => p.Id == propertyId, cancellationToken);
            if (propertySummery is null) return NoContent();

            //propertySummery.ImagesUrl = new List<string>();
            //foreach (var propertyImage in propertySummery.Images)
            //    propertySummery.ImagesUrl
            //        .Add(_pathProvider.GetImageApiPath<PropertyImage>(
            //            nameof(PropertyImage.ImageContentTumblr),
            //            propertyImage.Id.ToString()));
            //propertySummery.Images = null;

            return propertySummery;
        }
        
        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpGet("[Action]")]
        public async Task<ActionResult<CheckReadyToPublish>> CheckReadyToPublish(int propertyId, CancellationToken cancellationToken)
        {
            return await ModelService.AsQueryable(r => r.Id == propertyId)
                .Select(r =>
                new CheckReadyToPublish
                {
                    Title = !string.IsNullOrEmpty(r.Title),
                    Size = r.PropertyDetail != null,
                    Price = r.PropertyPrice != null,
                    Media = r.PropertyImage.Any(),
                }).FirstOrDefaultAsync(cancellationToken);
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> SetNotReadyForPublish([FromBody]PropertyDto propertyDto,
            CancellationToken cancellationToken)
        {
            var property = await ModelService.GetAsync(propertyDto.Id, cancellationToken);
            property.ReadyForPublishDate = null;
            property.ReadyForPublish = false;
            property.UserAccountIdReadyForPublish = null;
            await ModelService.UpdateAsync(property, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> SetReadyForPublish([FromBody]PropertyDto propertyDto,
            CancellationToken cancellationToken)
        {
            var property = await ModelService.Queryable.Include(a => a.PropertyPrice)
                .Include(a => a.PropertyLocation)
                .Include(a => a.PropertyDetail)
                .FirstOrDefaultAsync(p => p.Id == propertyDto.Id, cancellationToken: cancellationToken);
            if (property.PropertyPrice is null)
                throw new AppException("Price is empty. Please enter price in the price section");
            if (property.PropertyLocation is null)
                throw new AppException("Location is empty. Please enter location in the location section");
            if (property.PropertyDetail is null)
                throw new AppException("Property Detail is empty. Please enter property detail in the detail section");
            property.ReadyForPublish = true;
            property.ReadyForPublishDate = DateTime.Now;
            property.UserAccountIdReadyForPublish = _userProvider.Id;
            await ModelService.UpdateAsync(property, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> PublishProperty([FromBody]PropertyPaginationListDto propertyDto,
           CancellationToken cancellationToken)
        {
            //if (_userProvider.HasPublishingAuthorization != true)
            //    return Forbid();
            var property = await ModelService
                .GetAsync(p => p.Id == propertyDto.Id, cancellationToken);
            //if (!property.ReadyForPublish)
            //    throw new AppException("This property is not ready for publish");
            property.IsPublished = true;
            property.PublishingDate = DateTime.Now;
            property.UserAccountIdPublished = _userProvider.Id;
            await ModelService.UpdateAsync(property, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = UserGroups.Administrator + "," + UserGroups.RealEstateAdministrator + "," + UserGroups.Agent)]
        [HttpPut("[Action]")]
        public async Task<ActionResult> UnPublishProperty([FromBody]PropertyPaginationListDto propertyDto,
            CancellationToken cancellationToken)
        {
            if (_userProvider.HasPublishingAuthorization != true)
                return Forbid();
            var property = await ModelService
                .GetAsync(p => p.Id == propertyDto.Id, cancellationToken);
            property.IsPublished = false;
            property.PublishingDate = null;
            property.UserAccountIdPublished = null;
            await ModelService.UpdateAsync(property, cancellationToken);
            return NoContent();
        }

    }

}
