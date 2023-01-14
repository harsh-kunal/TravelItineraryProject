using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Payment:BaseDomainModel
    {
        public string PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public string PaymentDescription { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        //public int AccommodationId { get; set; }
        //public virtual Accommodation Accommodation { get; set; }
        //public int FlightId { get; set; }
        //public virtual Flight Flight { get; set; }
        //public int FligAccoId { get; set; }
        //public virtual FligAcco FligAcco { get; set; }
    }
}
