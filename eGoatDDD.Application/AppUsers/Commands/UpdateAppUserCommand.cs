using eGoatDDD.Application.AppUsers.Models;
using MediatR;
using System;

namespace eGoatDDD.Application.AppUsers.Commands
{
    public class UpdateAppUserCommand : IRequest<AppUserDto>
    {

        public UpdateAppUserCommand(AppUserDto AppUser)
        {
            AppUserId = AppUser.AppUserId;

            Role = AppUser.Role;

            FirstName = AppUser.FirstName;
            MiddleName = AppUser.MiddleName;
            LastName = AppUser.LastName;

            HomeAddress = AppUser.HomeAddress;
            HomeCity = AppUser.HomeCity;
            HomeRegion = AppUser.HomeRegion;
            HomeCountryCode = AppUser.HomeCountryCode;
            HomePhone = AppUser.HomePhone;

            Joined = AppUser.Joined;

            PackageId = AppUser.PackageId;
            IsActivated = AppUser.IsActivated;
        }

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
    }
}
