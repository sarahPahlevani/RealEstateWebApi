using RealEstateAgency.DAL.Contracts;
using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class Menu : IEntity, IForbiddenModify, ITranslatable<Menu>
    {
        public Menu Translate(int languageId)
        {
            throw new NotImplementedException();
        }
    }
}
