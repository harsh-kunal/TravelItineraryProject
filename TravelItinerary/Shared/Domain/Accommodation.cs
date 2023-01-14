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
        public double AccommodationFees { get; set; }
        public string AccommodationLocation { get; set; }
        public double AccommodationRating { get; set; }
        public int? TripId { get; set; }
        public virtual Trip Trip { get; set; }
        public int MyProperty { get; set; }
        public double PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

    }
}
