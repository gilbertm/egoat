using eGoatDDD.Application.Services.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Services.Commands
{
    public class CreateServiceCommand : IRequest<ServiceViewModel>
    {
        public long GoatId { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description { get; set; }
    }
}