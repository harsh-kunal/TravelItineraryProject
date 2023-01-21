using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Trip:BaseDomainModel, IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public int? Pax { get; set; }

        [Required]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
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
