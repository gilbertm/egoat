using eGoatDDD.Application.Users.Roles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eGoatDDD.Application.Users.Manages.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public SelectOptionList Roles { get; set; }
    }
}
