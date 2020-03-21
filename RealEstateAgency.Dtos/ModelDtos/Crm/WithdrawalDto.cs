using RealEstateAgency.DAL.DtoContracts;
using RealEstateAgency.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Dtos.ModelDtos.CRM
{
    public class WithdrawalDto : ModelDtoBase<Withdrawal>
    {
        public override int Id { get; set; }

        [Required]
        public int UserAccountId { get; set; }

        public UserAccount UserAccount { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }


        public override IModelDto<Withdrawal> From(Withdrawal entity)
        {
            Id = entity.Id;
            UserAccountId = entity.UserAccountId;
            UserAccount = entity.UserAccount;
            Amount = entity.Amount;
            DateCreated = entity.DateCreated;
            return this;
        }

        public override Withdrawal Create() =>
            new Withdrawal
            {
                UserAccountId = UserAccountId,
                Amount = Amount,
                DateCreated = DateCreated,
            };

        public override Withdrawal Update() =>
            new Withdrawal
            {
                Id = Id,
                UserAccountId = UserAccountId,
                Amount = Amount,
                DateCreated = DateCreated,
            };
    }
}
