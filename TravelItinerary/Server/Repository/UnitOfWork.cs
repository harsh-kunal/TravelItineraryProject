using TravelItinerary.Server.Data;
using TravelItinerary.Server.IRepository;
using TravelItinerary.Server.Models;
using TravelItinerary.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TravelItinerary.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Schedule> _schedules;
        private IGenericRepository<Event> _events;
        private IGenericRepository<Trip> _trips;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Accommodation> _accommodations;
        private IGenericRepository<Flight> _flights;
        private IGenericRepository<FligAcco> _fligaccos;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Schedule> Schedules
            => _schedules ??= new GenericRepository<Schedule>(_context);
        public IGenericRepository<Event> Events
            => _events ??= new GenericRepository<Event>(_context);
        public IGenericRepository<Trip> Trips
            => _trips ??= new GenericRepository<Trip>(_context);
        public IGenericRepository<Payment> Payments
            => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Customer> Customers
            => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Accommodation> Accommodations
            => _accommodations ??= new GenericRepository<Accommodation>(_context);
        public IGenericRepository<Flight> Flights
            => _flights ??= new GenericRepository<Flight>(_context);
        public IGenericRepository<FligAcco> FligAccos
            => _fligaccos ??= new GenericRepository<FligAcco>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            //string user = "System";
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            //foreach (var entry in entries)
            //{
            //    ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
            //    ((BaseDomainModel)entry.Entity).UpdatedBy = user.UserName;
            //    if (entry.State == EntityState.Added)
            //    {
            //        ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
            //        ((BaseDomainModel)entry.Entity).CreatedBy = user.UserName;
            //    }
            //}

            await _context.SaveChangesAsync();
        }
    }
}
