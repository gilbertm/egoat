using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Goats.Commands
{
    public class CreateGoatCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public long? MaternalId { get; set; }

        public long? SireId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public char Gender { get; set; }

        public string Picture { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }
    }
}