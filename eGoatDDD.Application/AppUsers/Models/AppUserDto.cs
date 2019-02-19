using System;
using System.Linq.Expressions;
using eGoatDDD.Domain.Entities;

namespace eGoatDDD.Application.AppUsers.Models
{
    public class AppUserDto
    {
        public string AppUserId { get; set; }

        public string Role { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string HomeRegion { get; set; }
        public string HomeCountryCode { get; set; }
        public string HomePhone { get; set; }

        public DateTime Joined { get; set; }

        public int PackageId { get; set; }
        public int IsActivated { get; set; }

        public static Expression<Func<AppUser, AppUserDto>> Projection
        {
            get
            {
                return p => new AppUserDto
                {
                    Role = p.Role,
                    AppUserId = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    HomeAddress = p.HomeAddress,
                    HomeCity = p.HomeCity,
                    HomeRegion = p.HomeRegion,
                    HomeCountryCode = p.HomeCountryCode,
                    HomePhone = p.HomePhone,
                    Joined = p.Joined,
                    PackageId = p.PackageId,
                    IsActivated = p.IsActivated
                };
            }
        }

        public static AppUserDto Create(AppUser AppUser)
        {
            return Projection.Compile().Invoke(AppUser);
        }
    }
}