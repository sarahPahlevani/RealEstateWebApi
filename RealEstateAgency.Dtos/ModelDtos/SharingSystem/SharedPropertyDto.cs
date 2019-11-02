using System;
using System.Collections.Generic;
using System.Text;
using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;

namespace RealEstateAgency.Dtos.ModelDtos.SharingSystem
{
    public class SharedPropertyDto : ModelDtoBase<SharedProperty>
    {
        public override int Id { get; set; }
        public int PropertyId { get; set; }
        public int? UserAccountId { get; set; }
        public int? SocialNetworkId { get; set; }
        public string RefererUrl { get; set; }
        public int ClickCount { get; set; }
        public string ReferralCodeAndSharingNetworkId { get; set; }

        public override IModelDto<SharedProperty> From(SharedProperty entity)
        {
            Id = entity.Id;
            PropertyId = entity.PropertyId;
            ReferralCodeAndSharingNetworkId = entity.ReferralCodeAndSharingNetworkId;
            UserAccountId = entity.UserAccountId;
            SocialNetworkId = entity.SocialNetworkId;
            RefererUrl = entity.RefererUrl;
            ClickCount = entity.ClickCount;
            return this;
        }

        public override SharedProperty Create() =>
            new SharedProperty
            {
                PropertyId = PropertyId,
                ReferralCodeAndSharingNetworkId = ReferralCodeAndSharingNetworkId,
                UserAccountId = UserAccountId,
                SocialNetworkId = SocialNetworkId,
                RefererUrl = RefererUrl,
                ClickCount = ClickCount,
            };

        public override SharedProperty Update() =>
            new SharedProperty
            {
                Id = Id,
                PropertyId = PropertyId,
                ReferralCodeAndSharingNetworkId = ReferralCodeAndSharingNetworkId,
                UserAccountId = UserAccountId,
                SocialNetworkId = SocialNetworkId,
                RefererUrl = RefererUrl,
                ClickCount = ClickCount,
            };
    }
}
