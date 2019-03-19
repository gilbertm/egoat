using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; }

        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }
}
