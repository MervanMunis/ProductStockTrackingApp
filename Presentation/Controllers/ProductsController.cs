using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _manager.ProductService.GetAllProducts(false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("prdoctById")]
        public IActionResult GetProductById([FromBody] Guid id)
        {
            try
            {
                var product = _manager.ProductService.GetProductById(id, false);

                if (product is null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneProduct([FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest();
                }

                _manager.ProductService.CreateProduct(product);

                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateOneProduct([FromRoute(Name = "id")] Guid id, [FromBody] Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest(); // 400
                }

                _manager.ProductService.UpdateProduct(id, product, true);

                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteOneProduct([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                _manager.ProductService.DeleteProduct(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
