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
    public class EventsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public EventsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetEvents()
        {
            //return await _context.Customers.ToListAsync();
            var eves = await _unitOfWork.Events.GetAll(includes: q => q.Include(x => x.Schedule).ThenInclude(h => h.Trip).ThenInclude(g => g.Customer));
            return Ok(eves);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetEvent(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var eve = await _unitOfWork.Events.Get(q => q.Id == id);

            if (eve == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(eve);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event eve)
        {
            if (id != eve.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Events.Update(eve);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await EventExists(id))
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
        public async Task<ActionResult<Customer>> PostEvent(Event eve)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Events.Insert(eve);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetEvent", new { id = eve.Id }, eve);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var eve = await _unitOfWork.Events.Get(q => q.Id == id);
            if (eve == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Events.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> EventExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var eve = await _unitOfWork.Events.Get(q => q.Id == id);
            return eve != null;
        }
    }
}
