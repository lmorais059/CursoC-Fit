using System.ComponentModel.DataAnnotations;
using System.Net;
using Catalog.Api.Contracts;
using Catalog.Domain.Data;
using Catalog.Domain.Errors;
using Catalog.Domain.Models;
using Catalog.Domain.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {
        // Query
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedData<Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedData<Customer>>> GetAll(
            [FromServices] IDataProvider database,
            [FromQuery] int page = 0,
            [FromQuery] int size = 5,
            [FromQuery] string? CustomerName = null
        )
        {
            IQueryable<Customer> query = database.Customers.AsQueryable();

            if (CustomerName is not null)
            {
                query = query.Where(c => c.Name.Contains(CustomerName));
            }

            IEnumerable<Customer> customers = await query
                .OrderBy(c => c.Id)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            int total = await query.CountAsync();

            PaginatedData<Customer> paginatedData = new(customers, size, page, total);

            return Ok(paginatedData);
        }

        // Query
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Customer>> GetById(
            [FromServices] IDataProvider database,
            [FromRoute] Guid id
        )
        {
            Customer? customer = await database.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                throw new AppError("Customer not found", HttpStatusCode.NotFound);
            }

            return Ok(customer);
        }

        // Commands
        [HttpPost]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(
            [FromServices] ICustomerService customerService,
            [FromBody] CustomerModel model
        )
        {
            Customer customer = await customerService.CreateAsync(model.Name, model.Email);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(
            [FromServices] ICustomerService customerService,
            [FromBody] CustomerModel model,
            [FromRoute] Guid id
        )
        {
            await customerService.UpdateAsync(id, model.Name, model.Email);
            return NoContent();
        }
    }

    public sealed record CustomerModel([Required] string Name, [Required] string Email);
}
