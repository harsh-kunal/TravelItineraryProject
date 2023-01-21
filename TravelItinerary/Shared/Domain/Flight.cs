﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Flight: BaseDomainModel, IValidatableObject
    {
        [Required]
        public string Airlines { get; set; }

        [Required]
        public string DestinationFrom { get; set; }

        public string DestinationTo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double FlighFees { get; set; }

        [Required]
        public int? Baggage { get; set; }

        [Required]
        public string CabinClass { get; set; }

        [Required]
        public double? FlightRating { get; set; }

        [Required]
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != null)
            {
                if (EndDate < StartDate)
                {
                    yield return new ValidationResult("End Date must be greater than Start Date");
                }
            }
        }
    }
}
