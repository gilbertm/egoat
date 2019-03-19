using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

/// <summary>
/// This is used for customizing the Shipped MVC Identity Authentication
/// </summary>
namespace eGoatDDD.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeRegion { get; set; }
        public string HomeCountryCode { get; set; }
        public string HomePhone { get; set; }

        public DateTime Joined { get; set; }

        public int IsActivated { get; set; }
    }
}