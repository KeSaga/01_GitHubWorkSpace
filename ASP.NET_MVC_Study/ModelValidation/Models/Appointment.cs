using ModelValidation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelValidation.Models
{
    public class Appointment
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string ClinetName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public bool TermsAccepted { get; set; }

    }
}