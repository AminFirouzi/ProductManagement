using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // POST api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        // GET api/product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();  // Create the query
            var result = await mediator.Send(query);  // Send the query to Mediator
            return Ok(result);  // Return all products
        }

        // PUT api/product/{manufactureEmail}/{produceDate}
        [HttpPut("{manufactureEmail}/{produceDate}")]
        public async Task<IActionResult> UpdateProduct(string manufactureEmail, DateTime produceDate, [FromBody] UpdateProductCommand command)
        {
            if (manufactureEmail != command.ManufactureEmail || produceDate != command.ProduceDate)
            {
                return BadRequest("Product key mismatch.");
            }

            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/product/{manufactureEmail}/{produceDate}
        [HttpDelete("{manufactureEmail}/{produceDate}")]
        public async Task<IActionResult> DeleteProduct(string manufactureEmail, DateTime produceDate)
        {
            var command = new DeleteProductCommand
            {
                ManufactureEmail = manufactureEmail,
                ProduceDate = produceDate
            };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
