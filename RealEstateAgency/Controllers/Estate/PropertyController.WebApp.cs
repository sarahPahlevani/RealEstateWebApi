using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateAgency.DAL.Models;
using RealEstateAgency.Dtos.ModelDtos.Estate;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos;
using RealEstateAgency.Implementations.ApiImplementations.PageDtos.PageFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateAgency.Controllers.Estate
{
    public partial class PropertyController
    {
        [AllowAnonymous]
        [HttpPost("GetWebAppPage")]
        public async Task<ActionResult<PageResultDto<PropertyWebAppListDto>>>
            GetWebAppPageAsync([FromBody] PageRequestFilterDto requestDto, CancellationToken cancellationToken)
        {
            var filter = requestDto.Filter.ToObject<PropertyListFilter>();
            //filter.SearchText = requestDto.Filter.ToString();
            var propertyPage = await new PageResultDto<PropertyWebAppListDto>(
                    filter.Filter(ModelService.Queryable.Where(p => p.IsPublished))
                        .Include(p => p.PropertyPrice)
                        .Include(p => p.PropertyDetail)
                        .Include(p => p.PropertyLocation)
                        .Select(p => new PropertyWebAppListDto
                        {
                            Id = p.Id,
                            PropertyType = p.PropertyType,
                            PropertyTypeId = p.PropertyTypeId,
                            PropertyLabel = p.PropertyLabel,
                            PropertyLabelId = p.PropertyLabelId,
                            PropertyStatus = p.PropertyStatus,
                            PropertyStatusId = p.PropertyStatusId,
                            Title = p.Title,
                            ZipCode = p.Title,
                            Price = p.PropertyPrice,
                            VideoUrl = p.VideoUrl,
                            Description = p.Description,
                            PropertyLocation = p.PropertyLocation,
                            PropertyDetail = p.PropertyDetail,
                            PropertyUniqId = p.PropertyUniqId,
                            Images = p.PropertyImage.Select(i => new PropertyWebAppImageDto
                            {
                                Id = i.Id,
                                Order = i.Priority,
                                ImagePath = i.ImagePath,
                                TumbPath = i.TumbPath,
                            }).ToList(),
                            PublishingDate = p.PublishingDate,
                        }), requestDto)
                .GetPage(cancellationToken);

            //foreach (var dto in propertyPage.Items)
            //{
            //    dto.ImagesId = dto.ImagesId.OrderBy(a => a.Order).ToList();
            //    dto.ImagesUrl = new List<string>();
            //    foreach (var i in dto.ImagesId)
            //    {
            //        dto.ImagesUrl.Add(
            //            _pathProvider.GetImageApiPath<PropertyImage>(
            //                nameof(PropertyImage.ImageContentTumblr), i.Id.ToString()));
            //    }
            //}

            return propertyPage;
        }

        [AllowAnonymous]
        [HttpGet("GetWebAppEstate/{propertyId}")]
        public async Task<ActionResult<WebSiteEstateDto>>
            GetWebAppEstate(int propertyId, CancellationToken cancellationToken)
        {
            var propertyPage = await ModelService
                .AsQueryable(p => p.IsPublished
                                  && p.Id == propertyId)
                .Include(p => p.PropertyPrice)
                .Include(p => p.PropertyDetail)
                .Include(p => p.PropertyLocation)
                .Include(p => p.PropertyInvolveFeature)
                .Include(p => p.PropertyAdditionalDetail)
                .Include(p => p.PropertyAttachment)
                .Select(p => new WebSiteEstateDto
                {
                    Id = p.Id,
                    PropertyTypeId = p.PropertyTypeId,
                    PropertyType = p.PropertyType,
                    PropertyLabelId = p.PropertyLabelId,
                    PropertyLabel = p.PropertyLabel,
                    PropertyStatusId = p.PropertyStatusId,
                    PropertyStatus = p.PropertyStatus,
                    Title = p.Title,
                    ZipCode = p.Title,
                    Price = p.PropertyPrice,
                    VideoUrl = p.VideoUrl,
                    Description = p.Description,
                    PropertyLocation = p.PropertyLocation,
                    PropertyDetail = p.PropertyDetail,
                    PropertyUniqId = p.PropertyUniqId,
                    AgentId = p.Request.RequestAgent.Any() ?
                        p.Request.RequestAgent.FirstOrDefault().AgentId : 0,
                    PublishingDate = p.PublishingDate,
                    Images = p.PropertyImage.Select(i => new PropertyWebAppImageDto
                    {
                        Id = i.Id,
                        Order = i.Priority,
                        ImagePath=i.ImagePath,
                        TumbPath=i.TumbPath,
                    }),
                    AdditionalDetails = p.PropertyAdditionalDetail.Select(a => new PropertyAdditionalDetailDto
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Value = a.Value
                    }),
                    Attachments = p.PropertyAttachment.Select(a => new PropertyAttachmentDto
                    {
                        Id = a.Id,
                        FileCaption = a.FileCaption,
                        FileExtension = a.FileExtension,
                        FileSize = a.FileSize,
                        UploadDate = a.UploadDate
                    }),
                    FeatureIds = p.PropertyInvolveFeature.Select(f => f.PropertyFeatureId),
                    FloorPlans = p.PropertyFloorPlan.Select(f => new PropertyFloorPlanDto
                    {
                        Id = f.Id,
                        Bathrooms = f.Bathrooms,
                        Bedrooms = f.Bedrooms,
                        UploadDate = f.UploadDate,
                        FloorName = f.FloorName,
                        FloorPrice = f.FloorPrice,
                        FloorSize = f.FloorSize,
                        ImageCaption = f.ImageCaption,
                        PropertyId = f.PropertyId,
                        ImageExtension = f.ImageExtension,
                        ImageSize = f.ImageSize,
                        PlanDescription = f.PlanDescription,
                        PricePostfix = f.PricePostfix,
                        SizePostfix = f.SizePostfix
                    })
                }).FirstOrDefaultAsync(cancellationToken);

            if (propertyPage is null) return NotFound();

            //propertyPage.ImagesId = propertyPage.ImagesId.OrderBy(a => a.Order).ToList();
            //propertyPage.ImagesUrl = new List<string>();
            //foreach (var i in propertyPage.ImagesId)
                //propertyPage.ImagesUrl.Add(i.ImagePath);
                //propertyPage.ImagesUrl.Add(
                //    _pathProvider.GetImageApiPath<PropertyImage>(
                //        nameof(PropertyImage.ImageContentFull), i.Id.ToString()));
            propertyPage.FloorPlans = propertyPage.FloorPlans.ToList();
            foreach (var i in propertyPage.FloorPlans)
                if (i.ImageSize > 0)
                    i.ImageContentFullUrl = _pathProvider.GetImageApiPath<PropertyImage>(
                            nameof(PropertyFloorPlan.ImageContentFull), i.Id.ToString());

            return propertyPage;
        }

        [HttpGet("[Action]")]
        [AllowAnonymous]
        public async Task<ActionResult<PageResultDto<PropertyWebAppListDto>>>
            AdvancedSearch(
                [FromQuery(Name = "page_number")] string pageNumber,
                [FromQuery(Name = "page_size")] string pageSize,
                [FromQuery(Name = "search")] string search,
                [FromQuery(Name = "type")] List<string> propertyTypes,
                [FromQuery(Name = "status")] List<string> propertyStatuses,
                [FromQuery(Name = "label")] List<string> propertyLabels,
                [FromQuery(Name = "feature")] List<string> propertyFeatures,
                [FromQuery(Name = "price_from")] string priceFrom,
                [FromQuery(Name = "price_to")] string priceTo,
                [FromQuery(Name = "size_from")] string sizeFrom,
                [FromQuery(Name = "size_to")] string sizeTo,
                [FromQuery(Name = "area_from")] string areaFrom,
                [FromQuery(Name = "area_to")] string areaTo,
                [FromQuery(Name = "rooms_from")] string roomsFrom,
                [FromQuery(Name = "rooms_to")] string roomsTo,
                [FromQuery(Name = "bed_rooms_from")] string bedRoomsFrom,
                [FromQuery(Name = "bed_rooms_to")] string bedRoomsTo,
                [FromQuery(Name = "bath_rooms_from")] string bathRoomsFrom,
                [FromQuery(Name = "bath_rooms_to")] string bathRoomsTo,
                [FromQuery(Name = "garages_from")] string garagesFrom,
                [FromQuery(Name = "garages_to")] string garagesTo,
                [FromQuery(Name = "garages_size_from")] string garagesSizeFrom,
                [FromQuery(Name = "garages_size_to")] string garagesSizeTo,
                [FromQuery(Name = "year_build_from")] string yearBuildFrom,
                [FromQuery(Name = "year_build_to")] string yearBuildTo,
                [FromQuery(Name = "floors_from")] string floorsFrom,
                [FromQuery(Name = "floors_to")] string floorsTo,
                [FromQuery(Name = "has_image")] string hasImage,
                [FromQuery(Name = "city")] string city,
                [FromQuery(Name = "lat")] string latitude,
                [FromQuery(Name = "long")] string longitude,
                CancellationToken cancellationToken)
        {
            var searchDto = CreateSearchDto(pageNumber,
            pageSize, search,
            propertyTypes, propertyStatuses,
            propertyLabels, propertyFeatures, priceFrom,
            priceTo, sizeFrom,
            sizeTo, areaFrom,
            areaTo, roomsFrom,
            roomsTo, bedRoomsFrom,
            bedRoomsTo, bathRoomsFrom,
            bathRoomsTo, garagesFrom,
            garagesTo, garagesSizeFrom,
            garagesSizeTo, yearBuildFrom,
            yearBuildTo, hasImage, city,
            floorsFrom, floorsTo,
            latitude, longitude);

            var propertyPage = await new PageResultDto<PropertyWebAppListDto>(
                    CreateSearchPredicate(searchDto, ModelService.AsQueryable(p => p.IsPublished)
                            .Include(p => p.PropertyPrice)
                            .Include(p => p.PropertyDetail)
                            .Include(p => p.PropertyLocation)
                            .Include(p => p.PropertyImage))
                        .Select(p => new PropertyWebAppListDto
                        {
                            Id = p.Id,
                            PropertyType = p.PropertyType,
                            PropertyTypeId = p.PropertyTypeId,
                            PropertyLabel = p.PropertyLabel,
                            PropertyLabelId = p.PropertyLabelId,
                            PropertyStatus = p.PropertyStatus,
                            PropertyStatusId = p.PropertyStatusId,
                            Title = p.Title,
                            ZipCode = p.Title,
                            Price = p.PropertyPrice,
                            VideoUrl = p.VideoUrl,
                            Description = p.Description,
                            PropertyLocation = p.PropertyLocation,
                            PropertyDetail = p.PropertyDetail,
                            PropertyUniqId = p.PropertyUniqId,
                            Images = p.PropertyImage.Where(i => !i.Deleted && !i.Is360View)
                                .Select(i => new PropertyWebAppImageDto
                                {
                                    Id = i.Id,
                                    Order = i.Priority,
                                    ImagePath = i.ImagePath,
                                    TumbPath = i.TumbPath,
                                }).ToList(),
                            PublishingDate = p.PublishingDate,
                        }), searchDto)
                .GetPage(cancellationToken);

            //foreach (var dto in propertyPage.Items)
            //{
            //    dto.ImagesId = dto.ImagesId.OrderBy(a => a.Order).ToList();
            //    dto.ImagesUrl = new List<string>();
            //    foreach (var i in dto.ImagesId)
            //        dto.ImagesUrl.Add(
            //            _pathProvider.GetImageApiPath<PropertyImage>(
            //                nameof(PropertyImage.ImageContentTumblr), i.Id.ToString()));
            //}

            return propertyPage;
        }

        private IQueryable<Property> CreateSearchPredicate(
            AdvancedSearchDto queryDto, IQueryable<Property> properties)
        {
            if(queryDto.HasImage > 0)
                properties = properties.Where(i => i.PropertyImage.Any());

            if (queryDto.PropertyTypeIds.Any())
                properties = properties.Where(i => queryDto.PropertyTypeIds.Contains(i.PropertyTypeId));
            if (queryDto.PropertyStatusIds.Any())
                properties = properties.Where(i => queryDto.PropertyStatusIds.Contains(i.PropertyStatusId));
            if (queryDto.PropertyLabelIds.Any())
                properties = properties.Where(i => queryDto.PropertyLabelIds.Contains(i.PropertyLabelId));
            if (queryDto.PropertyFeatureIds.Any())
                properties = properties.Where(i => i.PropertyInvolveFeature
                    .Any(f => queryDto.PropertyFeatureIds.Contains(f.PropertyFeatureId)));

            if (queryDto.RoomsFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Rooms >= queryDto.RoomsFrom);
            if (queryDto.RoomsTo != null)
                properties = properties.Where(i => i.PropertyDetail.Rooms <= queryDto.RoomsTo);

            if (queryDto.SizeFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Size >= queryDto.SizeFrom);
            if (queryDto.SizeTo != null)
                properties = properties.Where(i => i.PropertyDetail.Size <= queryDto.SizeTo);

            if (queryDto.AreaFrom != null)
                properties = properties.Where(i => i.PropertyDetail.LandArea >= queryDto.AreaFrom);
            if (queryDto.AreaTo != null)
                properties = properties.Where(i => i.PropertyDetail.LandArea <= queryDto.AreaTo);

            if (queryDto.BathRoomsFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Bathrooms >= queryDto.BathRoomsFrom);
            if (queryDto.BathRoomsTo != null)
                properties = properties.Where(i => i.PropertyDetail.Bathrooms <= queryDto.BathRoomsTo);

            if (queryDto.PriceFrom != null)
                properties = properties.Where(i => i.PropertyPrice.CalculatedPriceUnit >= queryDto.PriceFrom);
            if (queryDto.PriceTo != null)
                properties = properties.Where(i => i.PropertyPrice.CalculatedPriceUnit <= queryDto.PriceTo);

            if (queryDto.BathRoomsFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Bathrooms >= queryDto.BathRoomsFrom);
            if (queryDto.BathRoomsTo != null)
                properties = properties.Where(i => i.PropertyDetail.Bathrooms <= queryDto.BathRoomsTo);

            if (queryDto.BedRoomsFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Bedrooms >= queryDto.BedRoomsFrom);
            if (queryDto.BedRoomsTo != null)
                properties = properties.Where(i => i.PropertyDetail.Bedrooms <= queryDto.BedRoomsTo);

            if (queryDto.GaragesFrom != null)
                properties = properties.Where(i => i.PropertyDetail.Garages >= queryDto.GaragesFrom);
            if (queryDto.GaragesTo != null)
                properties = properties.Where(i => i.PropertyDetail.Garages <= queryDto.GaragesTo);

            if (queryDto.GaragesSizeFrom != null)
                properties = properties.Where(i => i.PropertyDetail.GaragesSize >= queryDto.GaragesSizeFrom);
            if (queryDto.GaragesSizeTo != null)
                properties = properties.Where(i => i.PropertyDetail.GaragesSize <= queryDto.GaragesSizeTo);

            if (queryDto.YearBuildFrom != null)
                properties = properties.Where(i => i.PropertyDetail.YearBuild >= queryDto.YearBuildFrom);
            if (queryDto.YearBuildTo != null)
                properties = properties.Where(i => i.PropertyDetail.YearBuild <= queryDto.YearBuildTo);

            if (queryDto.FloorsFrom != null)
                properties = properties.Where(i => i.PropertyFloorPlan.Count >= queryDto.FloorsFrom);
            if (queryDto.FloorsTo != null)
                properties = properties.Where(i => i.PropertyFloorPlan.Count <= queryDto.FloorsTo);

            if (queryDto.CityId != null)
                properties = properties.Where(i => i.PropertyLocation.CityId == queryDto.GaragesSizeFrom);

            if (queryDto.CityId != null)
                properties = properties.Where(i => i.PropertyLocation.CityId == queryDto.GaragesSizeFrom);

            if (!string.IsNullOrWhiteSpace(queryDto.Search))
                properties = properties.Where(p => p.Title.Contains(queryDto.Search) ||
                                                   p.PropertyLocation.AddressLine1.Contains(queryDto.Search) ||
                                                   p.PropertyLocation.AddressLine2.Contains(queryDto.Search) ||
                                                   p.PropertyLocation.ZipCode.Contains(queryDto.Search));

            return properties;
        }

        private AdvancedSearchDto CreateSearchDto(string pageNumber,
            string pageSize, string search,
            List<string> propertyTypes, List<string> propertyStatuses,
            List<string> propertyLabels, List<string> propertyFeatures, string priceFrom,
            string priceTo, string sizeFrom,
            string sizeTo, string areaFrom,
            string areaTo, string roomsFrom,
            string roomsTo, string bedRoomsFrom,
            string bedRoomsTo, string bathRoomsFrom,
            string bathRoomsTo, string garagesFrom,
            string garagesTo, string garagesSizeFrom,
            string garagesSizeTo, string yearBuildFrom,
            string yearBuildTo, string hasImage, string city, string floorsFrom, string floorsTo,
            string latitude, string longitude) =>
            new AdvancedSearchDto
            {
                PropertyTypeIds = ConvertToListOrDefault(propertyTypes),
                PropertyLabelIds = ConvertToListOrDefault(propertyLabels),
                PropertyStatusIds = ConvertToListOrDefault(propertyStatuses),
                PropertyFeatureIds = ConvertToListOrDefault(propertyFeatures),
                PageNumber = ConvertToIntOrDefault(pageNumber, 0).Value,
                PageSize = ConvertToIntOrDefault(pageSize, 10).Value,
                Search = search,
                PriceFrom = ConvertToIntOrDefault(priceFrom),
                PriceTo = ConvertToIntOrDefault(priceTo),
                SizeFrom = ConvertToIntOrDefault(sizeFrom),
                SizeTo = ConvertToIntOrDefault(sizeTo),
                AreaFrom = ConvertToIntOrDefault(areaFrom),
                AreaTo = ConvertToIntOrDefault(areaTo),
                RoomsFrom = ConvertToIntOrDefault(roomsFrom),
                RoomsTo = ConvertToIntOrDefault(roomsTo),
                BedRoomsFrom = ConvertToIntOrDefault(bedRoomsFrom),
                BedRoomsTo = ConvertToIntOrDefault(bedRoomsTo),
                BathRoomsFrom = ConvertToIntOrDefault(bathRoomsFrom),
                BathRoomsTo = ConvertToIntOrDefault(bathRoomsTo),
                GaragesFrom = ConvertToIntOrDefault(garagesFrom),
                GaragesTo = ConvertToIntOrDefault(garagesTo),
                GaragesSizeFrom = ConvertToIntOrDefault(garagesSizeFrom),
                GaragesSizeTo = ConvertToIntOrDefault(garagesSizeTo),
                YearBuildFrom = ConvertToIntOrDefault(yearBuildFrom),
                YearBuildTo = ConvertToIntOrDefault(yearBuildTo),
                CityId = ConvertToIntOrDefault(city),
                FloorsFrom = ConvertToIntOrDefault(floorsFrom),
                FloorsTo = ConvertToIntOrDefault(floorsTo),
                Latitude = ConvertToDecimalOrDefault(latitude),
                Longitude = ConvertToDecimalOrDefault(longitude),
                HasImage = ConvertToIntOrDefault(hasImage, 0).Value,
            };

        private int? ConvertToIntOrDefault(string input, int? defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(input)) return defaultValue;
            var parsed = int.TryParse(input, out var intValue);
            return parsed ? intValue : defaultValue;
        }
        private List<int> ConvertToListOrDefault(List<string> inputs)
        {
            var intList = new List<int>();
            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;
                if (int.TryParse(input, out var intValue)) intList.Add(intValue);
            }

            return intList;
        }

        private decimal? ConvertToDecimalOrDefault(string input, decimal? defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(input)) return defaultValue;
            var parsed = decimal.TryParse(input, out var intValue);
            return parsed ? intValue : defaultValue;
        }
    }
}
