using AutoMapper;
using Entities.DTOs.ProductDTO;
using Entities.DTOs.StockDTO;
using Entities.Models;

namespace Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product mappings
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductRequest, Product>();

            // Stock mappings
            CreateMap<Stock, StockResponse>().ReverseMap();
            CreateMap<StockRequest, Stock>();
        }
    }
}
