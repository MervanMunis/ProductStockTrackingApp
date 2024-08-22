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
            try
            {
                var products = await _manager.ProductService.GetAllProductsAsync(false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveProducts()
        {
            var products = await _manager.ProductService.GetProductsByIsDeletedStatusAsync(false, false);
            return Ok(products);
        }

        [HttpGet("deleted")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDeletedProducts()
        {
            var products = await _manager.ProductService.GetProductsByIsDeletedStatusAsync(true, false);
            return Ok(products);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetProductById([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                var product = await _manager.ProductService.GetProductByIdAsync(id, false);

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOneProduct([FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest is null)
                {
                    return BadRequest();
                }

                var createdProduct = await _manager.ProductService.CreateProductAsync(productRequest);

                return StatusCode(201, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOneProduct([FromRoute(Name = "id")] Guid id, [FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest is null)
                {
                    return BadRequest();
                }

                await _manager.ProductService.UpdateProductAsync(id, productRequest, true);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOneProduct([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                await _manager.ProductService.DeleteProductAsync(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
