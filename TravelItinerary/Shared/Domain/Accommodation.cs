using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class Accommodation:BaseDomainModel
    {
        public string AccommodationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AccommodationType { get; set; }
        public int AccommodationFees { get; set; }
        public string AccommodationLocation { get; set; }
        public int AccommodationRating { get; set; }
        public int FligAccoId { get; set; }
        public virtual FligAcco FligAcco { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public int MyProperty { get; set; }

    }
}
