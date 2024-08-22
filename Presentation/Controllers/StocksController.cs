using Entities.DTOs.ProductDTO;
using Entities.DTOs.StockDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

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

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var stocks = await _manager.StockService.GetAllStocksAsync(false);
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetStockById([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                var stock = await _manager.StockService.GetStockByIdAsync(id, false);

                if (stock is null)
                {
                    return NotFound();
                }

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockRequest stockRequest)
        {
            try
            {
                if (stockRequest is null)
                {
                    return BadRequest();
                }

                var createdStock = await _manager.StockService.CreateStockAsync(stockRequest);

                return StatusCode(201, createdStock);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetProductStockReport()
        {
            try
            {
                var report = await _manager.StockService.GetProductStockReportAsync(false);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateStock([FromRoute(Name = "id")] Guid id, [FromBody] StockRequest stockRequest)
        {
            try
            {
                if (stockRequest == null)
                {
                    return BadRequest();
                }

                await _manager.StockService.UpdateStockAsync(id, stockRequest, true);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteStock([FromRoute(Name = "id")] Guid id)
        {
            try
            {
                await _manager.StockService.DeleteStockAsync(id, false);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
