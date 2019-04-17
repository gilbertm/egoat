using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Goats.Commands
{
    public class DeleteGoatCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public int Type { get; set; }

        public string Reason { get; set; }

        public DateTime DisposedOn { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; } = DateTime.Now;

    }
}