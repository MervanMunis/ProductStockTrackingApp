using Entities.DTOs.ProductDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ProductsController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _manager.ProductService.GetAllProductsAsync(false);

            if (!result.Success)
                return Problem(result.ErrorMessage);

            return Ok(result.Data);            
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveProducts()
        {
            var result = await _manager.ProductService.GetActiveProductsAsync(false, false);

            if (!result.Success)
                return Problem(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetProductById([FromRoute(Name = "id")] Guid id)
        {
            var result = await _manager.ProductService.GetProductByIdAsync(id, false);

            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOneProduct([FromBody] ProductRequest productRequest)
        {
            var result = await _manager.ProductService.CreateProductAsync(productRequest);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);
            
            return Ok(result.Data);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneProduct([FromRoute(Name = "id")] Guid id, [FromBody] ProductRequest productRequest)
        {
            var result = await _manager.ProductService.UpdateProductAsync(id, productRequest, true);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneProduct([FromRoute(Name = "id")] Guid id)
        {
            var result = await _manager.ProductService.DeleteProductAsync(id, false);

            if (!result.Success)
                return NotFound(result.ErrorMessage);
            
            return Ok(result.Data);
        }
    }
}
