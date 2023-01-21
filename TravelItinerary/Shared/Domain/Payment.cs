using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Payment:BaseDomainModel
    {
        [Required]
        public string PaymentType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double PaymentAmount { get; set; }

        [Required]
        public string PaymentDescription { get; set; }

        [Required]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
