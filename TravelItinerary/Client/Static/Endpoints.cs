using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelItinerary.Client.Static
{
    public class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string CustomersEndpoint = $"{Prefix}/customers";
        public static readonly string TripsEndpoint = $"{Prefix}/trips";
        public static readonly string EventsEndpoint = $"{Prefix}/events";
        public static readonly string FlightsEndpoint = $"{Prefix}/flights";
        public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
        public static readonly string FligAccosEndpoint = $"{Prefix}/fligaccos";
        public static readonly string AccommodationsEndpoint = $"{Prefix}/accommodations";
        public static readonly string SchedulesEndpoint = $"{Prefix}/schedules";
        public static readonly string AccountsEndpoint = $"{Prefix}/accounts";
    }
}
