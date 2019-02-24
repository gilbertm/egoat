using eGoatDDD.Application.Colors.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;

namespace eGoatDDD.Application.Colors.Commands
{
    public class CreateColorCommand : IRequest<ColorViewModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}