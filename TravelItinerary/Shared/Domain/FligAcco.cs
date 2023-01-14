using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class FligAcco:BaseDomainModel
    {
        public double PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public double FlighAccoFees { get; set; }
        public string Airlines { get; set; }
        public string AccommodationName { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
