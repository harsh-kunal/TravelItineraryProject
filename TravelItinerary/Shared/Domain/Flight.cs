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
        public int FlighFees { get; set; }
        public int Baggage { get; set; }
        public string CabinClass { get; set; }
        public int FlightRating { get; set; }
        public int FligAccoId { get; set; }
        public virtual FligAcco FligAcco { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
