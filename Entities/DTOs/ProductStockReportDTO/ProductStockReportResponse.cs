using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ProductStockReportDTO
{
    public class ProductStockReportResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Guid StockId { get; set; }
        public int StockQuantity { get; set; }
        public DateTime StockCreationTime { get; set; }
    }
}
