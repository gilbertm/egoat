using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Goats.Commands
{
    public class RestoreGoatCommand : IRequest<bool>
    {
        public long Id { get; set; }

    }
}