using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelItinerary.Server.Data;
using TravelItinerary.Server.IRepository;
using TravelItinerary.Shared.Domain;

namespace TravelItinerary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public TripsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetTrips()
        {
            //return await _context.Customers.ToListAsync();
            var trips = await _unitOfWork.Trips.GetAll(includes: q => q.Include(x =>x.Customer));
            return Ok(trips);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetTrip(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var trip = await _unitOfWork.Trips.Get(q => q.Id == id);

            if (trip == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(trip);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Trips.Update(trip);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await TripExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trip>> PostTrip(Trip trip)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Trips.Insert(trip);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetTrip", new { id = trip.Id }, trip);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var trip = await _unitOfWork.Trips.Get(q => q.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Trips.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> TripExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var trip = await _unitOfWork.Trips.Get(q => q.Id == id);
            return trip != null;
        }
    }
}