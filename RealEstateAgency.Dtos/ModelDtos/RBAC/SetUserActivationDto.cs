using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateAgency.Dtos.ModelDtos.RBAC
{
    public class SetUserActivationDto
    {
        public int UserId { get; set; }
        public bool ActivationState { get; set; }
    }
}
