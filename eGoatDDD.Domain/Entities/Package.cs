using System.Collections.Generic;

namespace eGoatDDD.Domain.Entities
{
    public class Package
    {
        public Package()
        {
            AppUsers = new HashSet<AppUser>();
        }

        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        // allowed active maximum
        public int MaxLessee { get; set; }
        // allowed active total for all lessee
        public int Total { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; private set; }
    }
}
