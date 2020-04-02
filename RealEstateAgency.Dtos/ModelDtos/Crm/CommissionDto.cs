using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.CRM
{
    public class CommissionDto : ModelDtoBase<Commission>
    {
        public override int Id { get; set; }

        [Required]
        public byte CommissionPercent { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public bool IsPay { get; set; }

        public string PayCode { get; set; }

        public DateTime? PayDate { get; set; }


        public Request Request { get; set; }
        public UserAccount UserAccount { get; set; }
        public Property Property { get; set; }


        public override IModelDto<Commission> From(Commission entity)
        {
            Id = entity.Id;
            Request = entity.IdNavigation;
            CommissionPercent = entity.CommissionPercent;
            Amount = entity.Amount;
            DateCreated = entity.DateCreated;
            IsPay = entity.IsPay;
            PayCode = entity.PayCode;
            PayDate = entity.PayDate;
            //UserAccount = entity.IdNavigation.UserAccountIdSharedNavigation;
            //Property = entity.IdNavigation.PropertyNavigation;
            return this;
        }

        public override Commission Create() =>
            new Commission
            {
                Id = Id,
                CommissionPercent = CommissionPercent,
                Amount = Amount,
                DateCreated = DateCreated,
                IsPay = IsPay,
            };

        public override Commission Update() =>
            new Commission
            {
                Id = Id,
                CommissionPercent = CommissionPercent,
                Amount = Amount,
                DateCreated = DateCreated,
                IsPay = IsPay,
                PayCode = PayCode,
                PayDate = PayDate,
            };
    }
}
