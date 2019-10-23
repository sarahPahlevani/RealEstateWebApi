using System;
using System.Collections.Generic;

namespace RealEstateAgency.DAL.Models
{
    public partial class AuthenticationProvider
    {
        public AuthenticationProvider()
        {
            UserAccount = new HashSet<UserAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
