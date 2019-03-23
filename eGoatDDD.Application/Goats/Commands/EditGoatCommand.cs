using eGoatDDD.Application.Goats.Models;
using eGoatDDD.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Application.Goats.Commands
{
    public class EditGoatCommand : IRequest<bool>
    {
        public long Id { get; set; }

        public int ColorId { get; set; }

        public long? MaternalId { get; set; }

        public long? SireId { get; set; }

        public long? DisposalId { get; set; }

        public string Code { get; set; }

        public char Gender { get; set; }

        public ICollection<int> BreedId { get; set; }

        public ICollection<float> BreedPercent { get; set; }
        
        public IList<IFormFile> Files { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Description { get; set; }
    }
}