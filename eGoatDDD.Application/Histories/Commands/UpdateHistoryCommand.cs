using eGoatDDD.Application.Services.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Histories.Commands
{
    public class UpdateHistoryCommand : IRequest<ServiceViewModel>
    { 
        public long ServiceId { get; set; }
        public long GoatId { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Description { get; set; }
    }
}