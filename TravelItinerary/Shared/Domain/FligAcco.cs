using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelItinerary.Shared.Domain
{
    public class FligAcco:BaseDomainModel
    {
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
