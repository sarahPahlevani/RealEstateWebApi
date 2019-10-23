using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class PropertyAttachment
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int? UserAccountIdDeleteBy { get; set; }
        public string FileCaption { get; set; }
        public DateTime UploadDate { get; set; }
        public string FileExtension { get; set; }
        public int FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual Property Property { get; set; }
        public virtual UserAccount UserAccountIdDeleteByNavigation { get; set; }
    }
}
