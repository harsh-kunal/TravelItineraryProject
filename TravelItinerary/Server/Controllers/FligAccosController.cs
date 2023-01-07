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
    public class FligAccosController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public FligAccosController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetFligAccos()
        {
            //return await _context.Customers.ToListAsync();
            var fligaccos = await _unitOfWork.FligAccos.GetAll();
            return Ok(fligaccos);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetFligAcco(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var fligacco = await _unitOfWork.FligAccos.Get(q => q.Id == id);

            if (fligacco == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(fligacco);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFligAcco(int id, FligAcco fligacco)
        {
            if (id != fligacco.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.FligAccos.Update(fligacco);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await FligAccoExists(id))
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
        public async Task<ActionResult<FligAcco>> PostFligAcco(FligAcco fligacco)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.FligAccos.Insert(fligacco);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetFligAcco", new { id = fligacco.Id }, fligacco);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFligAcco(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var fligacco = await _unitOfWork.FligAccos.Get(q => q.Id == id);
            if (fligacco == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.FligAccos.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> FligAccoExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var fligacco = await _unitOfWork.FligAccos.Get(q => q.Id == id);
            return fligacco != null;
        }
    }
}
