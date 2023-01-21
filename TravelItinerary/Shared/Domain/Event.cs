using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Event:BaseDomainModel, IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string EventDescription { get; set; }

        [Required]
        public string EventLocation { get; set; }

        [Required]
        public int? ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
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
