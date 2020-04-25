using RealEstateAgency.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class Menu : IEntity, IForbiddenModify, ITranslatable<Menu>
    {
        public Menu Translate(int languageId)
        {
            return new Menu
            {
                Id = Id,
                Name = MenuTranslate.Any(t => t.MenuId == Id && t.LanguageId == languageId)
                    ? MenuTranslate.First().Name
                    : Name
                
            };
        }
    }
}
