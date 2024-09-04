using Entities.DTOs.ProductDTO;
using Entities.DTOs.StockDTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Exceptions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public StocksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllStocks()
        {
            var result = await _manager.StockService.GetAllStocksAsync(false);

            if (!result.Success)
                return Problem(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("active")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveStocks()
        {
            var result = await _manager.StockService.GetAllActiveStocksAsync(false, false);

            if (!result.Success)
                return Problem(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetStockById([FromRoute(Name = "id")] Guid id)
        {
            var result = await _manager.StockService.GetStockByIdAsync(id, false);

            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStock([FromBody] StockRequest stockRequest)
        {
            var result = await _manager.StockService.CreateStockAsync(stockRequest);
            
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpGet("report")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProductStockReport()
        {
            var result = await _manager.StockService.GetProductStockReportAsync(false);
            
            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStock([FromRoute(Name = "id")] Guid id, [FromBody] StockRequest stockRequest)
        {
            var result = await _manager.StockService.UpdateStockAsync(id, stockRequest, true);

            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStock([FromRoute(Name = "id")] Guid id)
        {
            var result = await _manager.StockService.DeleteStockAsync(id, false);

            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
