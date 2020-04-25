using RealEstateAgency.DAL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAgency.DAL.Models
{
    public partial class ContentPage : IEntity, ITranslatable<ContentPage>
    {
     
        public ContentPage Translate(int languageId)
        {
            return new ContentPage
            {
                Id = Id,
                Title = ContentPageTranslate.Any(t => t.ContentPageId == Id && t.LanguageId == languageId)
                   ? ContentPageTranslate.First().Title
                   : Title,
                ContentHeader = ContentPageTranslate.Any(t => t.ContentPageId == Id && t.LanguageId == languageId)
                   ? ContentPageTranslate.First().ContentHeader
                   : ContentHeader,
                ContectDetail = ContentPageTranslate.Any(t => t.ContentPageId == Id && t.LanguageId == languageId)
                   ? ContentPageTranslate.First().ContectDetail
                   : ContectDetail,
                ContentFooter = ContentPageTranslate.Any(t => t.ContentPageId == Id && t.LanguageId == languageId)
                   ? ContentPageTranslate.First().ContentFooter
                   : ContentFooter,
                
            };
        }
    }
}
