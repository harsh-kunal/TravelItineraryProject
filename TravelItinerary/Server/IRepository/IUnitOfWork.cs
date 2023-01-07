using TravelItinerary.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelItinerary.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Schedule> Schedules { get; }
        IGenericRepository<Event> Events { get; }
        IGenericRepository<Trip> Trips { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Accommodation> Accommodations { get; }
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Flight> Flights { get; }
        IGenericRepository<FligAcco> FligAccos { get; }
    }
}