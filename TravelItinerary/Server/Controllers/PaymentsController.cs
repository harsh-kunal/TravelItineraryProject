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
    public class PaymentsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public CustomersController(ApplicationDbContext context)
        public PaymentsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetPayments()
        {
            //return await _context.Customers.ToListAsync();
            var payments = await _unitOfWork.Payments.GetAll();
            return Ok(payments);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<IActionResult> GetPayment(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            //return customer;
            return Ok(payment);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Payments.Update(payment);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(id))
                if (!await PaymentExists(id))
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
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Insert(payment);
            await _unitOfWork.Save(HttpContext);


            return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Payments.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool CustomerExists(int id)
        private async Task<bool> PaymentExists(int id)
        {
            //return _context.Customers.Any(e => e.Id == id);
            var payment = await _unitOfWork.Payments.Get(q => q.Id == id);
            return payment != null;
        }
    }
}