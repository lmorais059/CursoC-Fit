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
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        // Queries
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromServices] IDataProvider database)
        {
            IEnumerable<Product> products = await database.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetById([FromServices] IDataProvider database, [FromRoute] Guid id)
        {
            Product? product = await database.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                throw new AppError("Product not found", HttpStatusCode.NotFound);
            }

            return Ok(product);
        }

        // Commands
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Product>> Create([FromServices] IProductService productService, [FromBody] ProductModel model)
        {
            Product product = await productService.CreateAsync(model.CustomerId, model.Name, model.Price);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorContract), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IProductService productService,
            [FromBody] UpdateProductModel model,
            [FromRoute] Guid id)
        {
            await productService.UpdateAsync(id, model.Name, model.Price);
            return NoContent();
        }

    }

    public sealed record ProductModel([Required] string Name, [Required] decimal Price, [Required] Guid CustomerId);
    public sealed record UpdateProductModel([Required] string Name, [Required] decimal Price);

}