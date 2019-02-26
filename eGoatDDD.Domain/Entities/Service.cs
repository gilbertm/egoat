﻿using System;
using System.ComponentModel.DataAnnotations;

namespace eGoatDDD.Domain.Entities
{
    public class Service
    {

        public long Id { get; set; }

        // medical
        // recreational
        public string Type { get; set; }

        // medical - vaccine
        // medical - deworming
        public string Category { get; set; }

        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime End { get; set; }


        public virtual Goat Goat { get; set; }

    }
}
