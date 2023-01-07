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
    public class SchedulesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public SchedulesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetSchedules()
        {
            //return await _context.Customers.ToListAsync();
            var schedules = await _unitOfWork.Schedules.GetAll(includes: q => q.Include(x =>x.Trip));
            return Ok(schedules);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetSchedule(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var schedule = await _unitOfWork.Schedules.Get(q => q.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(schedule);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Schedules.Update(schedule);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await ScheduleExists(id))
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
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Schedules.Insert(schedule);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var schedule = await _unitOfWork.Schedules.Get(q => q.Id == id);
            if (schedule == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Schedules.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> ScheduleExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var schedule = await _unitOfWork.Schedules.Get(q => q.Id == id);
            return schedule != null;
        }
    }
}