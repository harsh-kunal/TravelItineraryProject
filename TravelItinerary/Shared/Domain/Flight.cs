using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Flight: BaseDomainModel
    {
        public string Airlines { get; set; }
        public string DestinationFrom { get; set; }
        public string DestinationTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double FlighFees { get; set; }
        public int Baggage { get; set; }
        public string CabinClass { get; set; }
        public double FlightRating { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public double PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
