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
    public class AccommodationsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public AccommodationsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetAccommodations()
        {
            //return await _context.Customers.ToListAsync();
            var accommodations = await _unitOfWork.Accommodations.GetAll(includes: q => q.Include(x => x.Trip).ThenInclude(h => h.Customer).Include(x => x.Payment));
            return Ok(accommodations);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetAccommodation(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var accommodation = await _unitOfWork.Accommodations.Get(q => q.Id == id);

            if (accommodation == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(accommodation);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccommodation(int id, Accommodation accommodation)
        {
            if (id != accommodation.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Accommodations.Update(accommodation);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await AccommodationExists(id))
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
        public async Task<ActionResult<Accommodation>> PostAccommodation(Accommodation accommodation)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Accommodations.Insert(accommodation);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetAccommodation", new { id = accommodation.Id }, accommodation);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var accommodation = await _unitOfWork.Accommodations.Get(q => q.Id == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Accommodations.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> AccommodationExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var accommodation = await _unitOfWork.Accommodations.Get(q => q.Id == id);
            return accommodation != null;
        }
    }
}